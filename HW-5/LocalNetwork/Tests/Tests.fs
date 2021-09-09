namespace Tests

open LocalNetwork
open NUnit.Framework
open FsUnit
open Foq
open System.Collections.Generic

module Tests =

    let mockOS (infectionProbability: float) =
        Mock<IOS>()
            .Setup(fun os -> <@ os.InfectionProbability @>)
            .Returns(infectionProbability)
            .Create()

    type BadComputer() =
        let mutable isInfected = false

        interface IComputer with
            member _.OS = mockOS 1.0
            member _.getInfected() = isInfected <- true
            member _.IsInfected
                with get() = isInfected

    type GoodComputer() =
        let mutable isInfected = false

        interface IComputer with
            member _.OS = mockOS 0.0
            member _.getInfected () = ()
            member _.IsInfected
                with get() = isInfected

    let mockInfectedComputer () =
        Mock<IComputer>()
            .Setup(fun computer -> <@ computer.IsInfected @>)
            .Returns(true)
            .Setup(fun computer -> <@ computer.OS @>)
            .Returns(mockOS(0.0))
            .Setup(fun computer -> <@ computer.getInfected() @>)
            .Returns(())
            .Create()

    let makeNetwork (a: IComputer) (b: IComputer list) (c: IComputer list) =

        //       1
        //     /   \
        //   2 ----- 3
        //  /      /   \
        // 4      5     6

        let adjacency = Dictionary<IComputer, IComputer list>()

        adjacency.Add (a, b)

        adjacency.Add (b.[0], [a; b.[1]; c.[0]])
        adjacency.Add (b.[1], [a; b.[0]; c.[1]; c.[2]])

        adjacency.Add (c.[0], [b.[0]])
        adjacency.Add (c.[1], [b.[1]])
        adjacency.Add (c.[2], [b.[1]])

        let computers = [a::b; c] |> List.concat

        Network(computers, adjacency)


    [<Test>]
    let ``Should mimic BFS if all computers are bad`` () =

        let infected = mockInfectedComputer ()
        let firstLayer = List.init 2 (fun _ -> BadComputer() :> IComputer)
        let secondLayer = List.init 3 (fun _ -> BadComputer() :> IComputer)

        let network = makeNetwork infected firstLayer secondLayer

        network.IsOver() |> should equal false
        network.Update() |> should equal [true; true; true; false; false; false]
        network.IsOver() |> should equal false
        network.Update() |> should equal [true; true; true; true; true; true]
        network.IsOver() |> should equal true

    [<Test>]
    let ``Shouldn't change if all the computers are good`` () =

        let infected = mockInfectedComputer ()
        let firstLayer = List.init 2 (fun _ -> GoodComputer() :> IComputer)
        let secondLayer = List.init 3 (fun _ -> GoodComputer() :> IComputer)

        let network = makeNetwork infected firstLayer secondLayer

        network.IsOver() |> should equal true
        network.Update() |> should equal [true; false; false; false; false; false]

