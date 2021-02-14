let fibonacci x =
  if x < 0I then
      failwith $"Invalid input: {x}. Input value must be nonnegative."

  let rec fibonacciLoop (first, second) i =
    if i < x
    then fibonacciLoop (first + second, first) (i + 1I)
    else first

  fibonacciLoop (0I, 1I) 0I

printfn "15th fibonacci number: %A, 0th fibonacci number: %A" (fibonacci 15I) (fibonacci 0I)