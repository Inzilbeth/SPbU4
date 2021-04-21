namespace MiniCrawler

open System.IO
open System.Net
open System.Text.RegularExpressions

/// Module with crawler functions
module Crawler =
    let regex = Regex("<a href\s*=\s*\"?(https?://[^\"]+)\"?\s*>", RegexOptions.Compiled)

    /// Finds all the addresses in the form of <a href="http://...">.
    let findAllAdresses (page: string) =
        [for m in regex.Matches page -> m.Groups.[1].Value]

    let processAddress (address: string) =
        async {
            try
                let request = WebRequest.Create address
                use! response = request.AsyncGetResponse()
                use stream = response.GetResponseStream()
                use reader = new StreamReader(stream)
                return Some (reader.ReadToEnd())
            with
                | _ -> return None
        }

    /// Gets page lengths of all addresses listed on the original address
    let getLenghts (address: string) =
        match (processAddress address |> Async.RunSynchronously) with
        | Some page -> let addresses = findAllAdresses page
                       let lengths = List.map (fun a -> processAddress a) addresses
                                     |> Async.Parallel |> Async.RunSynchronously
                                     |> Seq.map (fun s -> if s.IsSome then s.Value.Length else 0)
                                     |> List.ofSeq
                       Some (List.zip addresses lengths)

        | None -> None