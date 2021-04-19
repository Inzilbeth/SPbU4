namespace Task2

module Lib =
    let supermap func list =
        let rec loop list acc =
            match list with
            | head::tail -> loop tail (List.append acc head)
            | [] -> acc

        loop (List.map func list) []
