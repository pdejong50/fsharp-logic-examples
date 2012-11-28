﻿// ========================================================================= //
// Copyright (c) 2003-2007, John Harrison.                                   //
// Copyright (c) 2012 Eric Taucher, Jack Pappas, Anh-Dung Phan               //
// (See "LICENSE.txt" for details.)                                          //
// ========================================================================= //

module FSharpx.Books.AutomatedReasoning.Tests.eqelim

open FSharpx.Books.AutomatedReasoning.lib
open FSharpx.Books.AutomatedReasoning.formulas
open FSharpx.Books.AutomatedReasoning.prop
open FSharpx.Books.AutomatedReasoning.fol
open FSharpx.Books.AutomatedReasoning.skolem
open FSharpx.Books.AutomatedReasoning.tableaux
open FSharpx.Books.AutomatedReasoning.meson
open FSharpx.Books.AutomatedReasoning.equal
open FSharpx.Books.AutomatedReasoning.eqelim
open NUnit.Framework
open FsUnit

// eqelim.p001
[<Test>]
let ``Abelian problem``() =
    meson002 
      (parse @"(forall x. P(1,x,x)) /\ 
              (forall x. P(x,x,1)) /\ 
              (forall u v w x y z. P(x,y,u) /\ 
              P(y,z,w) ==> (P(x,w,v) <=> P(u,z,v)))
                  ==> forall a b c. P(a,b,c) 
              ==> P(b,a,c)")
    |> should equal [13]

// eqelim.p002
[<Test>]
let ``Lemma for equivalence elimination``() =
    meson002 
      (parse @"(forall x. R(x,x)) /\ 
              (forall x y. R(x,y) ==>  R(y,x)) /\ 
              (forall x y z. R(x,y) /\ R(y,z) ==> R(x,z))
              <=> (forall x y. R(x,y) <=> (forall z. R(x,z) <=> R(y,z)))")
    |> should equal [4; 3; 9; 3; 2; 7]

// eqelim.p003
// Wishnu #1
[<Test; Category("LongRunning")>]
let ``examples 1``() =
    bmeson 
      (parse @"(exists x. x = f(g(x)) /\ forall x'. x' = f(g(x')) ==> x = x') <=>
              (exists y. y = g(f(y)) /\ forall y'. y' = g(f(y')) ==> y = y')")
    |> should equal [25; 25]

// eqelim.p004
// Wishnu #1
[<Test>]
let ``examples 2``() =
    emeson 
      (parse @"(exists x. x = f(g(x)) /\ forall x'. x' = f(g(x')) ==> x = x') <=>
              (exists y. y = g(f(y)) /\ forall y'. y' = g(f(y')) ==> y = y')")
    |> should equal [16; 16]

// eqelim.p005
[<Test; Category("LongRunning")>]
let ``examples 3``() =
    bmeson 
        (parse @"(forall x y z. x * (y * z) = (x * y) * z) /\ (forall x. e * x = x) /\ (forall x. i(x) * x = e) ==> forall x. x * i(x) = e")
    |> should equal [19]



