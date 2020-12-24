namespace AoC2020.Code

open System.Text.RegularExpressions

module Passports =
    let GetMatchOption(input: string, pattern: string) = 
        let rmatch = Regex.Match(input, pattern) in 
            match rmatch.Success with
            | true -> Some rmatch.Groups.["val"].Value
            | false -> None

    type WeakPassport = 
        struct
            val BirthYear: string option
            val IssueYear: string option
            val ExpireYear: string option
            val Height: string option
            val HairColour: string option
            val EyeColour: string option
            val PassportId: string option
            val CountryId: string option
            new(passport: string) = 
                    {BirthYear = GetMatchOption(passport, "byr:(?<val>\S+)");
                    IssueYear = GetMatchOption(passport, "iyr:(?<val>\S+)");
                    ExpireYear = GetMatchOption(passport, "eyr:(?<val>\S+)");
                    Height = GetMatchOption(passport, "hgt:(?<val>\S+)");
                    HairColour = GetMatchOption(passport, "hcl:(?<val>\S+)");
                    EyeColour = GetMatchOption(passport, "ecl:(?<val>\S+)");
                    PassportId = GetMatchOption(passport, "pid:(?<val>\S+)");
                    CountryId = GetMatchOption(passport, "cid:(?<val>\S+)")}
        end

        member this.IsValid = 
            match (this.BirthYear, this.IssueYear, this.ExpireYear, this.Height, 
                   this.HairColour, this.EyeColour, this.PassportId, this.CountryId) with
            | (Some _, Some _, Some _, Some _, Some _, Some _, Some _, _) -> true
            | _ -> false


    type Height = Cm of int | Inch of int

    let GetHeight (str: string) =
        match GetMatchOption(str, "(?<val>\d+)cm") with
        | Some x -> Cm (x |> int) |> Some
        | None -> 
            match GetMatchOption(str, "(?<val>\d+)in") with
            | Some x -> Inch (x |> int) |> Some
            | None -> None

    type StrictPassport = 
        struct
            val BirthYear: int option
            val IssueYear: int option
            val ExpireYear: int option
            val Height: Height option
            val HairColour: string option
            val EyeColour: string option
            val PassportId: string option
            val CountryId: string option
            new(passport: string) = 
                    {BirthYear = GetMatchOption(passport, "byr:(?<val>\d{4})") |> Option.map int;
                    IssueYear = GetMatchOption(passport, "iyr:(?<val>\d{4})") |> Option.map int;
                    ExpireYear = GetMatchOption(passport, "eyr:(?<val>\d{4})") |> Option.map int;
                    Height = GetHeight(passport);
                    HairColour = GetMatchOption(passport, "hcl:(?<val>#[0-9a-f]{6})");
                    EyeColour = GetMatchOption(passport, "ecl:(?<val>[amb|blu|brn|gry|grn|hzl|oth]{1})");
                    PassportId = GetMatchOption(passport, "pid:(?<val>\d+)");
                    CountryId = GetMatchOption(passport, "cid:(?<val>\d+)")}
        end

        member this.IsValid = 
            match (this.BirthYear, this.IssueYear, this.ExpireYear, this.Height, 
                   this.HairColour, this.EyeColour, this.PassportId, this.CountryId) with
            | (Some _, Some _, Some _, 
               Some _, Some _, Some _, Some _, _) -> 
                this.IsBirthYearValid 
                   && this.IsIssueYearValid 
                   && this.IsExpireYearValid 
                   && this.IsHeightValid
                   && this.IsPassportIdValid
            | _ -> false

        member this.IsBirthYearValid = 
            match this.BirthYear with
            | None -> false
            | Some x -> x >= 1920 && x <= 2002

        member this.IsIssueYearValid = 
            match this.IssueYear with
            | None -> false
            | Some x -> x >= 2010 && x <= 2020

        member this.IsExpireYearValid = 
            match this.ExpireYear with
            | None -> false
            | Some x -> x >= 2020 && x <= 2030

        member this.IsHeightValid = 
            match this.Height with
            | None -> false
            | Some (Cm x) -> x >= 150 && x <= 193
            | Some (Inch x) -> x >= 59 && x <= 76

        member this.IsPassportIdValid =
            match this.PassportId with
            | None -> false
            | Some x -> x.Length = 9

