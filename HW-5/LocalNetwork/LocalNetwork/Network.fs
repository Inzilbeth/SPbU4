namespace LocalNetwork

open System.Collections.Generic

/// Local network where computers infect each other every frame.
type Network (computers: IComputer list, adjacency: IDictionary<IComputer, IComputer list>) =
    let isActive (c: IComputer) =
        c.IsInfected && adjacency.[c] |> List.exists (fun n -> not n.IsInfected && n.OS.InfectionProbability > 0.0)

    /// Updates the state of the network.
    member _.Update () =
        computers |> List.filter isActive
                  |> List.map (fun c -> adjacency.[c])
                  |> List.concat
                  |> List.iter (fun c -> c.getInfected())

        computers |> List.map (fun c -> c.IsInfected)

    /// Checks whether the network reached its final state or not.
    member _.IsOver () =
        computers |> List.exists isActive |> not
