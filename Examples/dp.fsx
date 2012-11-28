﻿// ========================================================================= //
// Copyright (c) 2003-2007, John Harrison.                                   //
// Copyright (c) 2012 Eric Taucher, Jack Pappas, Anh-Dung Phan               //
// (See "LICENSE.txt" for details.)                                          //
// ========================================================================= //

#load "initialization.fsx"

open FSharpx.Books.AutomatedReasoning.lib
open FSharpx.Books.AutomatedReasoning.formulas
open FSharpx.Books.AutomatedReasoning.prop
open FSharpx.Books.AutomatedReasoning.propexamples
open FSharpx.Books.AutomatedReasoning.defcnf
open FSharpx.Books.AutomatedReasoning.dp

// pg. 84
// ------------------------------------------------------------------------- //
// Examples.                                                                 //
// ------------------------------------------------------------------------- //

// dp.p001
// Harrison #05 - prime
tautology(prime 11);;

// dp.p002
// Harrison #05 - prime
dptaut(prime 11);;

// pg. 85
// ------------------------------------------------------------------------- //
// Example.                                                                  //
// ------------------------------------------------------------------------- //

// dp.p003
// Harrison #05 - prime
dplltaut(prime 11);;

// pg. 89
// ------------------------------------------------------------------------- //
// Examples.                                                                 //
// ------------------------------------------------------------------------- //

// dp.p004
// Harrison #05 - prime
// Real: 00:05:30.392, CPU: 00:05:30.031, GC gen0: 1458, gen1: 137, gen2: 4
dplitaut(prime 101);;

// dp.p005
// Harrison #05 - prime
// Real: 00:01:37.614, CPU: 00:01:37.453, GC gen0: 426, gen1: 14, gen2: 1
dplbtaut(prime 101);;