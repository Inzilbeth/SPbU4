module Program

open System
open System.IO
open System.Collections.Generic
open System.Runtime.Serialization.Formatters.Binary

type PhoneBook () =

    member _.Add book number name =
        let check = System.Text.RegularExpressions.Regex("^[0-9-+ ]+$").IsMatch

        if check number then
            Map.add number name book
        else
            printfn "Number has wrong format!"
            book

    member _.FindNumbers book name =
        let numbers = Map.filter (fun _ value -> value = name) book
                        |> Map.toList
                        |> List.map (fun x -> fst x)

        if not numbers.IsEmpty then
            Some numbers
        else
            None

    member _.FindName (book:Map<string, string>) number =
        if (Map.exists (fun key _ -> key = number) book) then
            Some book.[number]
        else
            None

    member _.SaveToFile book =
        use fileStream = new FileStream("PhoneBook.txt", FileMode.Create)
        let formatter = BinaryFormatter()
        formatter.Serialize(fileStream, box book)

    member _.OpenFromFile book name =
        let merge (firstMap:Map<string, string>) (secondMap:Map<string, string>) =
            let rec split first second =
                match first with
                | head::tail -> let newMap = Map.filter (fun key _ -> key <> (fst head)) second
                                split tail newMap

                |_ -> second

            let splitted = split (Map.toList firstMap) secondMap
            Map(Seq.concat [(Map.toSeq firstMap); (Map.toSeq splitted)])

        try
            use fileStream = new FileStream(name, FileMode.Open)
            let formatter = BinaryFormatter()
            let result : Map<string, string> = formatter.Deserialize(fileStream) |> unbox
            merge result book
        with
            | :? FileNotFoundException -> printfn "File not found"; book


[<EntryPoint>]
let main _ =
    let book = PhoneBook()
    let contacts = Map.empty

    let rec mainLoop contacts =
        printfn "1: Exit"
        printfn "2: Add contact (name & number)"
        printfn "3: Search number"
        printfn "4: Search name"
        printfn "5: Print all contacts"
        printfn "6: Save to file"
        printfn "7: Read from file"

        let command = Console.ReadLine()

        match command with
        | "1" ->
            printfn "Application closed"
            Environment.Exit(0)

        | "2" ->
            printfn "Enter name: "
            let name = Console.ReadLine()
            printfn "Enter number: "
            let number = Console.ReadLine()

            if (Map.exists (fun key _ -> key = number) contacts) then
                printfn "Number is used!"
                mainLoop contacts
            else
                mainLoop <| book.Add contacts number name
                printfn "Added successfully!"

        | "3" ->
            printfn "Enter name: "
            let name = Console.ReadLine()

            let found = book.FindNumbers contacts name

            if found.IsNone then
               printfn "Name not found!"
            else
               printfn "Results: "
               List.iter (fun i -> printfn "%A" i ) found.Value

            mainLoop contacts

        | "4" ->
            printfn "Enter number: "
            let number = Console.ReadLine()

            let found = book.FindName contacts number

            if found.IsNone then
                printfn "Number not found!"
            else
                printfn "Result: "
                printfn "%A" found.Value

            mainLoop contacts

        | "5" ->
            if contacts.IsEmpty then
                printfn "Book is empty!"
            else
                printfn "Book entries: "
                Map.iter (fun key value -> printfn "Name: %A | Number: %A " value key) <| contacts

            mainLoop contacts

        | "6" ->
            book.SaveToFile(contacts)
            printfn("Saved!")

            mainLoop contacts

        | "7" ->
            printfn "Enter file name: "
            let name = Console.ReadLine()
            printfn("Opened!")

            mainLoop <|book.OpenFromFile contacts name

        | _ ->
            printfn "Command not found!"

            mainLoop contacts

    mainLoop contacts
    0
