INCLUDE Globals.ink
VAR numQuestions = 0
hmmmm...#speaker:Diane #type:internal
{objectsCompleted <5: ->too_early | ->start}
===too_early===
It's not time for this one yet #speaker:Harold #type:external
->END
===start=== 
{
-numCorrectGuesses<1: Oh… maybe we should stop, Honey. It seems like you’re having a tough time today. #speaker:Harold #type:external
        Let me try just one more, please. #speaker:Diane
        Okay Honey, just one more. #speaker:Harold
        ->question_knot
    - numCorrectGuesses<4: Oh I just found this one! But it might be too tricky… I mean, I barely recognize it myself. Do you want to try it? #speaker:Harold #type:external
        I can do it! I’ll just need some help. #speaker:Diane
        I… think I can do this. #type:internal
        ->question_knot
    -else: Oh here’s one more! It might be a really tricky one… but you’ve been doing so well, so I’m sure you’ll remember this one.#speaker:Harold #type:external
        I have no idea what the heck this thing even is. #speaker:Diane #type:internal
        ->question_knot
}
    
===question_knot===
{
    -numQuestions<2:
        Where could this be from? #speaker:Diane #type:internal
  - numQuestions<3:
       Oh this trip was very recent, I know that… but do I know where it is? #speaker:Diane #type:internal
   - else:
        It’s getting late, Honey. Why don’t you tell me where you think we got this and then we can go on that walk?#speaker:Harold #type:external
}
*[What the heck is this thing?]
   We were both surprised to see this guy, but when we asked a local they told us that it’s the country’s mascot! #speaker:Harold #type:external
   ~nextVersion(true)
   ~numQuestions=numQuestions+1
   ->question_knot
*[What do you remember about this trip?]
     It was one of the most amazing cities I’ve ever been to. The city was so clean and the architecture was stunning. #Speaker:Harold #type:external
     And nature was incorporated right into the city itself: there were buildings covered in plants, many zoos, and you could see animals making their homes in the same places as humans. It was unlike anywhere else in the world.#Speaker:Harold #type:external
     ~nextVersion(true)
     ~numQuestions=numQuestions+1
   ->question_knot
*[When did we go here?]
    Heh *Chuckles*, We watched that Crazy Rich Asians movie and as soon as the credits rolled, we looked at each other and said, “Let’s go there next.” A few months later, we booked a trip.#speaker:Harold #type:external
    ~nextVersion(true)
    ~numQuestions=numQuestions+1
   ->question_knot
*->
//this line calls a function in Unity to enter the mode for guessing the place
    ~enterGuessMode(true)
    -> holdingKnot
    
===holdingKnot===
Now where were we?#speaker:Harold
Where was it?#speaker:Diane #type:internal
->holdingKnot

===correctAnswer===
Yes! Singapore! This was our last vacation before COVID, and I’m so glad that we went before the Alzheimer’s set in. It was an incredible experience, and I couldn’t have possibly done it without my best gal by my side. #speaker:Harold #type:external
Oh right… this is the Merlion. I remember seeing him all around the country, but the best one was the fountain that shot water right into the river. #speaker:Diane
Oh yes, that was the best one! We had gotten some food and had a little picnic on the steps nearby. That was a wonderful day. #speaker:Harold
I… yes, I remember. That was a wonderful day. #speaker:Diane
~numCorrectGuesses=numCorrectGuesses+1
->endings

===incorrectAnswer===
No, but that’s okay Honey, don’t beat yourself up, this one was tricky. This is a Merlion, we saw a bunch of these statues when we visited Singapore a few years ago. #speaker:Harold #type:external
Gosh, a few years ago? I don’t think I remember that. #speaker:Diane
Harold: Well, Dr. Wasserman said your short-term memory would be the first thing to go from the Alzheimer’s, so maybe that’s why. #speaker:Harold
Maybe… I just wish I remembered. I’m sure we had a wonderful time. #speaker:Diane
We did… we had the time of our lives. #speaker:Harold
->endings

===endings===
{
    - numCorrectGuesses < 2: 
        ->one_correct
    - numCorrectGuesses < 6:
        ->two_correct
    -else:
        ->all_correct
}

===one_correct===
oh...*Sniffle* *Sniffle* #speaker:Diane #type:external
Oh come here, Honey. #speaker:Harold
I’ve lost so much already, Bear. I could barely remember anything, and some of these trips weren’t that long ago!#speaker:Diane
This was really hard. Maybe I should have given you more clues.#speaker:Harold
I shouldn’t have needed this many clues? I used to teach geography for God’s sake! I’ve seen the world, and I don’t even remember it.#speaker:Diane
Doctor Wasserman said that with Alzheimer’s there are good days and bad days. Maybe today was just a bad day.#speaker:Harold
But how many good days do I have left?#speaker:Diane
I don’t know, Honey, but I will cherish them when you have them.#speaker:Harold
I will too. I just don’t want to leave you. Not yet.#speaker:Diane
I know, D. But you’re still with me today, and that’s all I can ask for. Now let’s go on that walk, huh?#speaker:Harold
~endGame(true)
->END
===two_correct===
That wasn’t too bad, you got a few right.#speaker:Harold
It was so hard to remember all of these trips. I can’t believe that I’ve lost so much already.#speaker:Diane
Dr. Wasserman said that would happen, but if we keep doing exercises like this, he thinks you can keep your memories a little longer.#speaker:Harold
It’s not just the memories though, Harold… I could barely recognize any of these things when I picked them up, let alone remember where they’re from.#speaker:Diane
I… I guess I didn’t realize that.#speaker:Harold
It’s *Sigh* scary, Bear. I feel like I’m supposed to be smart and I’m supposed to remember all of these trips, but it’s like looking at a foreign language sometimes. #speaker:Diane
I just… I don’t want to look like an idiot in front of you.#speaker:Diane
Diane… I would never think that you’re an idiot. You’re one of the sharpest people I’ve ever known.#speaker:Harold
I know who you are, and just because you have Alzheimer’s doesn't erase the fact that I’ve never once beaten you in Scrabble or that you could name every world capital in alphabetical order.#speaker:Harold
 Well, I certainly can’t do that now!#speaker:Diane
That might be true, but I’ll always remember you as that person, even if you’re not that person anymore.#speaker:Harold
That’s very sweet, Bear.#speaker:Diane
We’ve had a very sweet life, Honey.#speaker:Harold
~endGame(true)
->END
===all_correct===
See, you did an amazing job, Honey! I really think everything is going to be okay.#speaker:Harold
I know, but Harold… it’s going to get worse.#speaker:Diane
Yes, but we’ll deal with that when it happens.#speaker:Harold
Bear, this was already challenging for me when it wouldn’t have been even a year ago. I couldn’t recognize any of these things when I first picked them up. #speaker:Diane
I already need so much help, and I’m only going to need more and more as the Alzheimer’s progresses.#speaker:Diane
And I’ll be here to help you, D.#speaker:Harold
I know you will, Bear. I know you will. But eventually, I’m not going to be me anymore, and we need to be ready for that.#speaker:Diane
Well… you’re you now, and that’s all I can ask for.#speaker:Harold
Yes. And I’m happy to be here with you now.#speaker:Diane
~endGame(true)
->END
