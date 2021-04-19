namespace Tests

open MiniCrawler.Crawler
open NUnit.Framework
open FsUnit

module Test =

    let expectedAddresses1 = [
        "http://corp.kaltura.com/products/video-platform-features"
        "http://corp.kaltura.com/Products/Features/Video-Management"
        "http://corp.kaltura.com/Video-Solutions"
        "http://corp.kaltura.com/Products/Features/Video-Player"
    ]

    let expectedAddresses2 = [
        "https://www.mediawiki.org/wiki/Special:MyLanguage/How_to_contribute"
        "https://stats.wikimedia.org/#/en.wikipedia.org"
        "https://foundation.wikimedia.org/wiki/Cookie_statement"
        "https://wikimediafoundation.org/"
        "https://www.mediawiki.org/"
    ]

    [<Test>]
    let ``Should find adresses correctly I`` () =
        let page = processAddress "https://bb.spbu.ru/" |> Async.RunSynchronously
        if page.IsSome then
            findAllAdresses page.Value |> should equal expectedAddresses1

    [<Test>]
    let ``Should find adresses correctly II`` () =
        let page = processAddress "https://en.wikipedia.org/wiki/F_Sharp_(programming_language)" |> Async.RunSynchronously
        if page.IsSome then
            findAllAdresses page.Value |> should equal expectedAddresses2

    [<Test>]
    let ``Should work in a simple case`` () =
        getLenghts "https://github.com/Inzilbeth"
                    |> should equal
                    <| Some [("https://docs.github.com/en/articles/blocking-a-user-from-your-personal-account", 52053)
                             ("https://docs.github.com/en/articles/reporting-abuse-or-spam", 54516)
                             ("https://docs.github.com/categories/setting-up-and-managing-your-github-profile", 86263)]