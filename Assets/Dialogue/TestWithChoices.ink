INCLUDE Globals.ink

{ name == "": -> main | -> already_chose}

=== main ===
Which Pokemon do you choose? This is to the make the line longer for testing purposes. It is not relevant.
    +[Charmander]
        -> chosen("Charmander")
    +[Bulbasaur]
        -> chosen("Bulbasaur")
    +[Squirtle]
        -> chosen("Squirtle")

=== chosen(pokemon) ===
~name = pokemon
You chose {pokemon}!
->END

=== already_chose ===
You already chose {name}!
-> END