# book0423 -  Book Registration and Book Search using F# WebSharper
![image](https://user-images.githubusercontent.com/16160120/164912972-f53ed29f-20ac-44ee-b179-a8bde3dc22d6.png)

## Overview
This is a sample program of F# program using F# WebSharper-SPA.
Database connection needs a client Server dataexchange and it is not implemented.
(Actually database is prepared and sql is not a problem. But could not find how to edit the WebSharper-web source code.)

## Requirement
Modern web browser.
Visual Studio 2022, Windows 10
Commands as followings:
```
> dotnet new -i WebSharper.Templates
> dotnet tool install -g dotnet-ws
> cd c:\<parent-folder-of-the-projects>
> dotnet new websharper-spa -lang f# -n <project-name>
> cd <project-name>
> dotnet add package WebSharper.UI
> dotnet tool install -g dotnet-ws
> dotnet add package WebSharper.JQuery
> dotnet ws build                                 ←Able to skip this line.
> dotnet run 
```

## Usage
**This is a sample program and cannot register to your database. When reloading your page, registered data will be disappeared.**  
### Register Books
If you try to simulate registering books, follow the steps below.  

1. Input a book data  
  Input the Title, Author, Published Year.  Year is a mondatory field.  If you not find the year, type 0  
  ![image](https://user-images.githubusercontent.com/16160120/164914424-b0d8ddc9-e84a-440c-b726-4592c00b9e52.png)
  ![image](https://user-images.githubusercontent.com/16160120/164914447-c8eb3799-d759-46d8-b88e-d78d177d1511.png)
  ![image](https://user-images.githubusercontent.com/16160120/164914465-15ed68e2-ce46-4bca-81b1-24e55674e2de.png)  

2. Then click \[Register\].  
  ![image](https://user-images.githubusercontent.com/16160120/164914002-dfd4b1a4-4952-4833-a96a-c089d3207ce6.png)  

3. Then, you can see the following line.  
  ![image](https://user-images.githubusercontent.com/16160120/164914571-79c64fc2-ba61-408b-ad37-4c1bba311237.png)  
  The Orange text line `Book "new book" by "new author" is added to the top.` is a message.  
  Your registered book is shown on top of the list.  

### Search Books  
1. Type title or author.  Currently year published is not effect to the result.  
  Text are case insensitive.  
  ![image](https://user-images.githubusercontent.com/16160120/164934604-b539cd56-c263-43c8-b3bb-0feb18a5362d.png)  

2. Then, search result is shown on the list bellow.  Text is case insensitive.  If there is no title/author which means any title/author.  If both title, author is not entered then all book list are shown.  
  ![image](https://user-images.githubusercontent.com/16160120/164934043-76511767-650d-41ec-97a6-b41bcf82a423.png)  

## Features
- Register books  
- Book Search:  
  Case insensitive.  
  Title = no text that means any title.  
  Author = no text that means any author.  
  Both Title and Author field has no text string, then all books are listed.  

## Reference  
 Book list data is created by the following web site.  
 The Greatest Books of All Time:   Source: https://thegreatestbooks.org/   

## Author  
Takahiro FUJIWARA  
https://www.linkedin.com/in/fujiwat/

