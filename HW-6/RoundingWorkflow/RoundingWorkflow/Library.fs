namespace RoundingWorkflow

open System

module Library =

    type Workflow(epsilon: int) =

        member workflow.Bind (n : float, func) =
            func (Math.Round(n, epsilon))

        member workflow.Return (n : float) =
            Math.Round(n, epsilon)
