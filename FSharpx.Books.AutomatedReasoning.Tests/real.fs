﻿// ========================================================================= //
// Copyright (c) 2003-2007, John Harrison.                                   //
// Copyright (c) 2012 Eric Taucher, Jack Pappas, Anh-Dung Phan               //
// (See "LICENSE.txt" for details.)                                          //
// ========================================================================= //

module FSharpx.Books.AutomatedReasoning.Tests.real

open NUnit.Framework
open FsUnit
open FSharpx.Books.AutomatedReasoning.lib
open FSharpx.Books.AutomatedReasoning.intro
open FSharpx.Books.AutomatedReasoning.formulas
open FSharpx.Books.AutomatedReasoning.prop
open FSharpx.Books.AutomatedReasoning.fol
open FSharpx.Books.AutomatedReasoning.skolem
open FSharpx.Books.AutomatedReasoning.completion
open FSharpx.Books.AutomatedReasoning.qelim
open FSharpx.Books.AutomatedReasoning.cooper
open FSharpx.Books.AutomatedReasoning.complex
open FSharpx.Books.AutomatedReasoning.real

(* ------------------------------------------------------------------------- *)
(* First examples.                                                           *)
(* ------------------------------------------------------------------------- *)

let private example_results_1 : formula<fol>[] = [|
    False;
    True;
    False;
    False;
    True;
    Or
     (And
       (Atom
         (R ("=",
           [Fn ("+", [Fn ("0", []); Fn ("*", [Var "a"; Fn ("1", [])])]);
            Fn ("0", [])])),
       Or
        (And
          (Atom
            (R ("=",
              [Fn ("+", [Fn ("0", []); Fn ("*", [Var "b"; Fn ("1", [])])]);
               Fn ("0", [])])),
          Atom
           (R ("=",
             [Fn ("+", [Fn ("0", []); Fn ("*", [Var "c"; Fn ("1", [])])]);
              Fn ("0", [])]))),
        And
         (Not
           (Atom
             (R ("=",
               [Fn ("+", [Fn ("0", []); Fn ("*", [Var "b"; Fn ("1", [])])]);
                Fn ("0", [])]))),
         Or
          (Atom
            (R (">",
              [Fn ("+", [Fn ("0", []); Fn ("*", [Var "b"; Fn ("1", [])])]);
               Fn ("0", [])])),
          Not
           (Atom
             (R (">",
               [Fn ("+", [Fn ("0", []); Fn ("*", [Var "b"; Fn ("1", [])])]);
                Fn ("0", [])]))))))),
     And
      (Not
        (Atom
          (R ("=",
            [Fn ("+", [Fn ("0", []); Fn ("*", [Var "a"; Fn ("1", [])])]);
             Fn ("0", [])]))),
      Or
       (And
         (Atom
           (R (">",
             [Fn ("+", [Fn ("0", []); Fn ("*", [Var "a"; Fn ("1", [])])]);
              Fn ("0", [])])),
         Or
          (Atom
            (R ("=",
              [Fn ("+",
                [Fn ("0", []);
                 Fn ("*",
                  [Var "a";
                   Fn ("+",
                    [Fn ("+",
                      [Fn ("0", []);
                       Fn ("*",
                        [Var "b";
                         Fn ("+",
                          [Fn ("0", []);
                           Fn ("*", [Var "b"; Fn ("-1", [])])])])]);
                     Fn ("*",
                      [Var "a";
                       Fn ("+",
                        [Fn ("0", []); Fn ("*", [Var "c"; Fn ("4", [])])])])])])]);
               Fn ("0", [])])),
          And
           (Not
             (Atom
               (R ("=",
                 [Fn ("+",
                   [Fn ("0", []);
                    Fn ("*",
                     [Var "a";
                      Fn ("+",
                       [Fn ("+",
                         [Fn ("0", []);
                          Fn ("*",
                           [Var "b";
                            Fn ("+",
                             [Fn ("0", []);
                              Fn ("*", [Var "b"; Fn ("-1", [])])])])]);
                        Fn ("*",
                         [Var "a";
                          Fn ("+",
                           [Fn ("0", []);
                            Fn ("*", [Var "c"; Fn ("4", [])])])])])])]);
                  Fn ("0", [])]))),
           Not
            (Atom
              (R (">",
                [Fn ("+",
                  [Fn ("0", []);
                   Fn ("*",
                    [Var "a";
                     Fn ("+",
                      [Fn ("+",
                        [Fn ("0", []);
                         Fn ("*",
                          [Var "b";
                           Fn ("+",
                            [Fn ("0", []);
                             Fn ("*", [Var "b"; Fn ("-1", [])])])])]);
                       Fn ("*",
                        [Var "a";
                         Fn ("+",
                          [Fn ("0", []); Fn ("*", [Var "c"; Fn ("4", [])])])])])])])
    ;
                 Fn ("0", [])])))))),
       And
        (Not
          (Atom
            (R (">",
              [Fn ("+", [Fn ("0", []); Fn ("*", [Var "a"; Fn ("1", [])])]);
               Fn ("0", [])]))),
        Or
         (Atom
           (R ("=",
             [Fn ("+",
               [Fn ("0", []);
                Fn ("*",
                 [Var "a";
                  Fn ("+",
                   [Fn ("+",
                     [Fn ("0", []);
                      Fn ("*",
                       [Var "b";
                        Fn ("+",
                         [Fn ("0", []); Fn ("*", [Var "b"; Fn ("-1", [])])])])]);
                    Fn ("*",
                     [Var "a";
                      Fn ("+",
                       [Fn ("0", []); Fn ("*", [Var "c"; Fn ("4", [])])])])])])]);
              Fn ("0", [])])),
         And
          (Not
            (Atom
              (R ("=",
                [Fn ("+",
                  [Fn ("0", []);
                   Fn ("*",
                    [Var "a";
                     Fn ("+",
                      [Fn ("+",
                        [Fn ("0", []);
                         Fn ("*",
                          [Var "b";
                           Fn ("+",
                            [Fn ("0", []);
                             Fn ("*", [Var "b"; Fn ("-1", [])])])])]);
                       Fn ("*",
                        [Var "a";
                         Fn ("+",
                          [Fn ("0", []); Fn ("*", [Var "c"; Fn ("4", [])])])])])])])
    ;
                 Fn ("0", [])]))),
          Atom
           (R (">",
             [Fn ("+",
               [Fn ("0", []);
                Fn ("*",
                 [Var "a";
                  Fn ("+",
                   [Fn ("+",
                     [Fn ("0", []);
                      Fn ("*",
                       [Var "b";
                        Fn ("+",
                         [Fn ("0", []); Fn ("*", [Var "b"; Fn ("-1", [])])])])]);
                    Fn ("*",
                     [Var "a";
                      Fn ("+",
                       [Fn ("0", []); Fn ("*", [Var "c"; Fn ("4", [])])])])])])]);
              Fn ("0", [])]))))))));
    False;
    True;
    |]

// real.p001
[<TestCase(@"exists x. x^4 + x^2 + 1 = 0", 0)>]

// real.p002
[<TestCase(@"exists x. x^3 - x^2 + x - 1 = 0", 1)>]

// real.p003
[<TestCase(@"exists x y. x^3 - x^2 + x - 1 = 0 /\
                         y^3 - y^2 + y - 1 = 0 /\ ~(x = y)", 2)>]

// real.p004
[<TestCase(@"exists x. x^2 - 3 * x + 2 = 0 /\ 2 * x - 3 = 0", 3)>]

// real.p005
[<TestCase(@"forall a f k. (forall e. k < e ==> f < a * e) ==> f <= a * k", 4)>]

// real.p006
[<TestCase(@"exists x. a * x^2 + b * x + c = 0", 5)>]

// real.p007
[<TestCase(@"forall a b c. (exists x. a * x^2 + b * x + c = 0) <=>
                           b^2 >= 4 * a * c", 6)>]

// real.p008
[<TestCase(@"forall a b c. (exists x. a * x^2 + b * x + c = 0) <=>
                           a = 0 /\ (b = 0 ==> c = 0) \/
                           ~(a = 0) /\ b^2 >= 4 * a * c", 7)>]

let ``examples 1`` (f, idx) =
    parse f
    |> real_qelim
    |> should equal example_results_1.[idx]


(* ------------------------------------------------------------------------- *)
(* Termination ordering for group theory completion.                         *)
(* ------------------------------------------------------------------------- *)

// real.p009
[<Test>]
let ``examples 2``() =
    @"1 < 2 /\ (forall x. 1 < x ==> 1 < x^2) /\
             (forall x y. 1 < x /\ 1 < y ==> 1 < x * (1 + 2 * y))"
    |> parse
    |> real_qelim
    |> should equal formula<fol>.True

(* ------------------------------------------------------------------------- *)
(* A case where using DNF is an improvement.                                 *)
(* ------------------------------------------------------------------------- *)

// real.p013
[<Test>]
let ``examples 3``() =
    @"forall d.
     (exists c. forall a b. (a = d /\ b = c) \/ (a = c /\ b = 1)
                            ==> a^2 = b)
     <=> d^4 = 1"
    |> parse
    |> real_qelim'
    |> should equal formula<fol>.True
