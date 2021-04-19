namespace PointFree.Library

module Library =

    let func x list =
        List.map (fun y -> y * x) list

    let func01 x =
        List.map (fun y -> y * x)

    let func02 x =
        List.map ((*) x)

    let func03 () =
        List.map << (*)
