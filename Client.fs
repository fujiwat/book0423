namespace book0423

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating

[<JavaScript>]
module Client =
    // The templates are loaded from the DOM, so you just can edit index.html
    // and refresh your browser, no need to recompile unless you add or remove holes.
    type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>

    // type for list
    type Entry =
        {
            Flag: string;
            Title: string;
            Author: string;
            Published: int;
        }
        static member Create flag title author published =
            {
                Flag = flag
                Title = title
                Author = author
                Published = published
            }

    [<SPAEntryPoint>]
    let Main () =
        // *****************************************************
        //  initial value for input field and caption
        // *****************************************************
        let newEntryTitle = Var.Create ""       // blank
        let newEntryAuthor = Var.Create ""      // blank
        let newEntryPublished = Var.Create 2022 // only the year is set
        let msg = Var.Create ""                // blank
        let cap = Var.Create "=== REGISTERED BOOKS ==="

        // *****************************************************
        // Permanent Book list
        // The Greatest Books of All Time:   Source: https://thegreatestbooks.org/
        // There was no year information.  just start from 1900
        // *****************************************************
        let allBooks =
            ListModel.FromSeq [
                Entry.Create "" "In Search of Lost Time" "Marcel Proust" 1900
                Entry.Create "" "Ulysses" "James Joyce" 1901
                Entry.Create "" "Don Quixote" "Miguel de Cervantes" 1902
                Entry.Create "" "One Hundred Years of Solitude" "Gabriel Garcia Marquez" 1903
                Entry.Create "" "The Great Gatsby" "F. Scott Fitzgerald" 1904
                Entry.Create "" "Moby Dick" "Herman Melville" 1905
                Entry.Create "" "War and Peace" "Leo Tolstoy" 1906
                Entry.Create "" "Hamlet" "William Shakespeare" 1907
                Entry.Create "" "The Odyssey" "Homer" 1908
                Entry.Create "" "Madame Bovary" "Gustave Flaubert" 1909
                Entry.Create "" "The Divine Comedy" "Dante Alighieri" 1910
                Entry.Create "" "Lolita" "Vladimir Nabokov" 1911
                Entry.Create "" "The Brothers Karamazov" "Fyodor Dostoyevsky" 1912
                Entry.Create "" "Crime and Punishment" "Fyodor Dostoyevsky" 1913
                Entry.Create "" "Wuthering Heights" "Emily Bront?" 1914
                Entry.Create "" "The Catcher in the Rye" "J. D. Salinger" 1915
                Entry.Create "" "Pride and Prejudice" "Jane Austen" 1916
                Entry.Create "" "The Adventures of Huckleberry Finn" "Mark Twain" 1917
                Entry.Create "" "Anna Karenina" "Leo Tolstoy" 1918
                Entry.Create "" "Alice's Adventures in Wonderland" "Lewis Carroll" 1919
                Entry.Create "" "The Iliad" "Homer" 1920
                Entry.Create "" "To the Lighthouse" "Virginia Woolf" 1921
                Entry.Create "" "Catch-22" "Joseph Heller" 1922
                Entry.Create "" "Heart of Darkness" "Joseph Conrad" 1923
                Entry.Create "" "The Sound and the Fury" "William Faulkner" 1924
                Entry.Create "" "Nineteen Eighty Four" "George Orwell" 1925
                Entry.Create "" "Great Expectations" "Charles Dickens" 1926
                Entry.Create "" "One Thousand and One Nights" "India/Iran/Iraq/Egypt" 1927
                Entry.Create "" "The Grapes of Wrath" "John Steinbeck" 1928
                Entry.Create "" "Absalom, Absalom!" "William Faulkner" 1929
                Entry.Create "" "Invisible Man" "Ralph Ellison" 1930
                Entry.Create "" "To Kill a Mockingbird" "Harper Lee" 1931
                Entry.Create "" "The Trial" "Franz Kafka" 1932
                Entry.Create "" "The Red and the Black" "Stendhal" 1933
                Entry.Create "" "Middlemarch" "George Eliot" 1934
                Entry.Create "" "Gulliver's Travels" "Jonathan Swift" 1935
                Entry.Create "" "Beloved" "Toni Morrison" 1936
                Entry.Create "" "Mrs. Dalloway" "Virginia Woolf" 1937
                Entry.Create "" "The Stories of Anton Chekhov" "Anton Chekhov" 1938
                Entry.Create "" "The Stranger" "Albert Camus" 1939
                Entry.Create "" "Jane Eyre" "Charlotte Bronte" 1940
                Entry.Create "" "The Aeneid" "Virgil" 1941
                Entry.Create "" "Collected Fiction" "Jorge Luis Borges" 1942
                Entry.Create "" "The Sun Also Rises" "Ernest Hemingway" 1943
                Entry.Create "" "David Copperfield" "Charles Dickens" 1944
                Entry.Create "" "Tristram Shandy" "Laurence Sterne" 1945
                Entry.Create "" "Leaves of Grass" "Walt Whitman" 1946
                Entry.Create "" "The Magic Mountain" "Thomas Mann" 1947
                Entry.Create "" "A Portrait of the Artist as a Young Man" "James Joyce" 1948
                Entry.Create "" "Midnight's Children" "Salman Rushdie" 1949
            ]

        // book list but for the display list
        let Books =
            ListModel.FromSeq []
        // initial value is copied from above permanent book list
        allBooks.Iter(fun t ->
            Entry.Create (t.Flag) (t.Title) (t.Author) (t.Published) 
            |> Books.Add )

        let row entry =
            IndexTemplate.Row()
                .Flag(entry.Flag)
                .Title(entry.Title)
                .Author(entry.Author)
                .Published(string entry.Published)
                .Doc()

        // *****************************************************
        // display book list
        // *****************************************************
        let data =
            Books.View.Doc(fun lm -> 
                lm 
(* Sort not needed.
                lm 
                |> Seq.sortBy (fun t -> 
                    t.Title
                ) 
*)
                |> Seq.map row
                |> Doc.Concat
            )

        // *****************************************************
        //  replace from index.html
        // *****************************************************
        IndexTemplate.Main()
            .Caption(cap.View)
            .Data(data)
            .Title(newEntryTitle)
            .Author(newEntryAuthor)
            .Published(newEntryPublished)
            .Msg(msg)
            // *****************************************************
            //  Regisiter button
            // *****************************************************
            .Reister(fun _ ->
                cap.Value <- "=== REGISTERED BOOKS ==="
                Books.Clear()
                // add the registered book to the top
                Entry.Create ("Registered") (newEntryTitle.Value) (newEntryAuthor.Value) (newEntryPublished.Value)
                |> Books.Add
                allBooks.Iter(fun t -> 
                    Entry.Create (t.Flag) (t.Title) (t.Author) (t.Published)
                    |> Books.Add 
                    )
                allBooks.Clear()
                Books.Iter(fun t -> 
                    Entry.Create (t.Flag) (t.Title) (t.Author) (t.Published)
                    |> allBooks.Add 
                    )
                // Display message.   registered.
                msg.Value <- ("Book \"" + (string newEntryTitle.Value) + "\" by \"" + (string newEntryAuthor.Value) + "\" is added to the top.")
                newEntryTitle.Value <- ""   // clear only the title.  available same author quick entry.
                )
            // *****************************************************
            //  Search button
            // *****************************************************
            .Search(fun _ ->
                cap.Value <- "=== BOOK SEARCH RESULT ==="
                Books.Clear()
                // *****************************************************
                //  copy from the permanent book list, if the string contains
                // *****************************************************
                allBooks.Iter(fun t ->
                    let bTitle:bool  = if  (newEntryTitle.Value.Trim().Equals("")) then true else (t.Title.ToLower().Contains(newEntryTitle.Value.ToLower()))
                    let bAuthor:bool = if  (newEntryAuthor.Value.Trim().Equals("")) then true else (t.Author.ToLower().Contains(newEntryAuthor.Value.ToLower()))
                    if bTitle && bAuthor then
                        Entry.Create (t.Flag) (t.Title) (t.Author) (t.Published)
                        |> Books.Add
                    )
                // Display message.   found.
                msg.Value <-
                    if (Books.Length = 0) then
                        Entry.Create ("") ("") ("") (0)
                        |> Books.Add
                        "Book \"" + newEntryTitle.Value + "\" by \"" + newEntryAuthor.Value + "\" is not found."
                    else
                        "Book \"" + newEntryTitle.Value + "\" by \"" + newEntryAuthor.Value + "\" is found, " + (string Books.Length) + 
                            (if Books.Length = 1 then " Book." else " Books.")                  
                )
            .Doc()
        |> Doc.RunById "main"

