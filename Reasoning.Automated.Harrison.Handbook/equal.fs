﻿// ========================================================================= //
// Copyright (c) 2003-2007, John Harrison.                                   //
// Copyright (c) 2012 Eric Taucher, Jack Pappas                              //
// (See "LICENSE.txt" for details.)                                          //
// ========================================================================= //

module Reasoning.Automated.Harrison.Handbook.equal

    open formulas
    open folMod
    open skolem

//
// pg. 235
// ========================================================================= //
// First order logic with equality.                                          //
// ========================================================================= //

    let is_eq = function 
        | Atom (R ("=", _)) -> true 
        | _ -> false

    let mk_eq s t =
        Atom (R ("=", [s; t]))

    let dest_eq fm =
        match fm with
        | Atom (R ("=", [s;t])) ->
            s, t
        | _ ->
            failwith "dest_eq: not an equation"

    let inline lhs eq = fst <| dest_eq eq
    let inline rhs eq = snd <| dest_eq eq

// pg. 239
// ------------------------------------------------------------------------- //
// The set of predicates in a formula.                                       //
// ------------------------------------------------------------------------- //

    let rec predicates fm =
        atom_union (fun (R (p, a)) -> [p, List.length a]) fm

// pg. 239
// ------------------------------------------------------------------------- //
// Code to generate equality axioms for functions.                           //
// ------------------------------------------------------------------------- //

    let function_congruence (f, n) =
        if n = 0 then [] 
        else
            // TODO : These instances of List.map could be optimized by using
            // List.init instead (so we don't need the intermediate list).
            let argnames_x = List.map (fun n -> "x" + (string n)) [1 .. n]
            let argnames_y = List.map (fun n -> "y" + (string n)) [1 .. n]
            let args_x = List.map Var argnames_x
            let args_y = List.map Var argnames_y
            let ant =
                (args_x, args_y)
                ||> List.map2 mk_eq
                |> List.reduceBack mk_and
            let con = mk_eq (Fn (f, args_x)) (Fn (f, args_y))
            [List.foldBack mk_forall (argnames_x @ argnames_y) (Imp (ant, con))]

// pg. 240
// ------------------------------------------------------------------------- //
// And for predicates.                                                       //
// ------------------------------------------------------------------------- //

    let predicate_congruence (p, n) =
        if n = 0 then []
        else
            // TODO : These instances of List.map could be optimized by using
            // List.init instead (so we don't need the intermediate list).
            let argnames_x =
                //List.init n <| fun n -> "x" + string (n + 1)
                List.map (fun n -> "x" + (string n)) [1 .. n]
            let argnames_y =
                //List.init n <| fun n -> "y" + string (n + 1)
                List.map (fun n -> "y" + (string n)) [1 .. n]
            let args_x = List.map Var argnames_x
            let args_y = List.map Var argnames_y
            let ant =
                (args_x, args_y)
                ||> List.map2 mk_eq
                |> List.reduceBack mk_and
            let con = Imp (Atom (R (p, args_x)), Atom (R (p, args_y)))
            [List.foldBack mk_forall (argnames_x @ argnames_y) (Imp (ant, con))]

// pg. 240
// ------------------------------------------------------------------------- //
// Hence implement logic with equality just by adding equality "axioms".     //
// ------------------------------------------------------------------------- //

    let equivalence_axioms =
        [(parse "forall x. x = x"); (parse "forall x y z. x = y /\ x = z ==> y = z")]

    let equalitize fm =
        let allpreds = predicates fm
        if not (mem ("=", 2) allpreds) then fm
        else
            let axioms =
                let preds = subtract allpreds ["=", 2]
                let funcs = functions fm

                equivalence_axioms
                |> List.foldBack (union << predicate_congruence) preds
                |> List.foldBack (union << function_congruence) funcs

            Imp (List.reduceBack mk_and axioms, fm)
