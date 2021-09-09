module Tests

open Program
open NUnit.Framework
open FsUnit
open System.IO

type Tests () =

    let book = Program.PhoneBook()

    [<Test>]
    member _.``Should add numbers correctly`` () =
        let expected = Map.empty.Add("96 58 55", "Alice")
                                .Add("89253651785", "Bob")
                                .Add("+7-977-777-35-62", "Carl")

        let actual =
            book.Add (book.Add (book.Add Map.empty
                "96 58 55" "Alice") "89253651785" "Bob") "+7-977-777-35-62" "Carl"

        actual |> should equal expected

    [<Test>]
    member _.``Should not add non-numbers`` () =
        let expected = Map.empty

        let actual =
            book.Add (book.Add (book.Add Map.empty
                "77f888" "Alice") "%aaa#@" "Bob") "text" "Carl"

        actual |> should equal expected

    [<Test>]
    member _.``Should find existing name`` () =
        let contacts = Map.empty.Add("96 58 55", "Alice")

        let result = book.FindName contacts "96 58 55"

        result |> should equal (Some "Alice")

    [<Test>]
    member _.``Should not find missing name`` () =
        let contacts = Map.empty.Add("96 58 55", "Alice")

        let result = book.FindName contacts "88005553535"

        result |> should equal None

    [<Test>]
    member _.``Should find existing numbers`` () =
        let contacts = Map.empty.Add("96 58 55", "Alice")
                                .Add("10801920", "Alice")

        let result = book.FindNumbers contacts "Alice"

        result |> should equal (Some ["10801920"; "96 58 55"])

    [<Test>]
    member _.``Should not find missing numbers`` () =
        let contacts = Map.empty.Add("96 58 55", "Alice")
                                .Add("10801920", "Alice")

        let result = book.FindNumbers contacts "Jackie"

        result |> should equal None

    [<Test>]
    member _.``Opening/saving to file should work correctly`` () =
        let expected = Map.empty.Add("96 58 55", "Alice")
                                .Add("89253651785", "Bob")
                                .Add("+7-977-777-35-62", "Carl")
        book.SaveToFile expected |> ignore
        let actual = book.OpenFromFile Map.empty "PhoneBook.txt"
        File.Delete "PhoneBook.txt"

        actual |> should equal expected
