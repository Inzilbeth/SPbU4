namespace LocalNetwork

/// Operating system that sets the infection probability for a computer.
type IOS =
    /// Infection probability.
    abstract member InfectionProbability: float

/// Interface for a computer in the network.
type IComputer =

    /// Computer's operating system.
    abstract member OS: IOS

    /// Whether computer is infected or not
    abstract member IsInfected: bool

    /// Tries to infect itself. Succeeds with a probability declared in OS.
    abstract member getInfected: unit -> unit
