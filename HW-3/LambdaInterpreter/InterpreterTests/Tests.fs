namespace LambdaInterpreter.Tests

open FsUnit
open NUnit.Framework
open LambdaInterpreter.Interpreter

[<TestFixture>]
module Tests =

    [<Test>]
    let ``Reducing II = I`` () =
        Interpreter.reduce
        <| Application(Abstraction('x', Variable 'x'), Abstraction('x', Variable 'x'))
        |> should equal <| Abstraction('x', Variable 'x')

    [<Test>]
    let ``Reducing KI = K*)``() =
        Interpreter.reduce
        <| (Application(Abstraction('x', Abstraction('y', Variable 'x')), Abstraction('x', Variable 'x')))
        |> should equal <| Abstraction('y', Abstraction('x', Variable 'x'))

    [<Test>]
    let ``Simple case reducing`` () =
        Interpreter.reduce
        <| Application(Abstraction('x', Variable 'x'), Abstraction('x', Variable 'x'))
        |> should equal (Abstraction('x', Variable 'x'))

    [<Test>]
    let ``Substitution test`` () =
        Interpreter.subst
            'x' (Variable 'z') (Application(Variable 'x', Variable 'y'))
        |> should equal (Application(Variable 'z', Variable 'y'))