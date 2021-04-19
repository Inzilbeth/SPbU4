namespace Test

module Square =
    let print n =
        let rec loop string total current =
            if current = 1 then
                string + "\n" + String.replicate total "*"
            else
                loop (string + "\n" + "*" + String.replicate (total - 2) " " + "*") total (current - 1)

        if (n > 1) then
            loop (String.replicate n "*") n (n - 1)
        else if (n = 1) then
                "*"
            else
                ""
