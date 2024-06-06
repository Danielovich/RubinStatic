Before writing this piece of software I had made a small effort to help out writing a routine
for creating blog comment links, for the blog owned by the Danish Software Quality author and programmer [Mark Seemann](https://blog.ploeh.dk). 

When looking at the repo that serves Marks blog, specifically the notion of how ["Posts" are kept and persisted](https://github.com/ploeh/ploeh.github.com/tree/master/_posts), I started thinking about how "clever" it would be host ones own blog content in a git repo, decentralized from whatever piece of software rendering those markdown files. One of Mark's clever ideas is that he utilizes Git as a comment mechanism, where one pull requests a comment to a post. 

> You get a lot "for free" by using Git for this. A draft mechanism (you can choose to not commit and push the posts you keep locally), an option for collaborative authoring by utilizing pull-requests and a central place to store your content which is not a database per se. 

Another thing about Mark's blog is that it is - as far I know - based on the static site generator Jekyll.

At the same time I have had a growing bothering issue with paying for a runtime on a cloud provider just because I needed a place to gather my writings. And hosting a static site is, by far, a cost reduction. There are other clear optimizations for serving static content - such as - it's fast! 

As I was starting to blog again, I had for some time used the [mataroa.blog](https://mataroa.blog) which I have been super happy with. It's a no bullshit offering; clean, simple, cheap and I might have kept using it if I did not find the templating options too poor to my taste. 

I tried for a few days to configure the [Mataroa code](https://github.com/mataroa-blog/mataroa) on my dev-machine and wanted to implement a different option for configuring templates, but I found the dependencies too noisy on my machine, and I had a hard time (I am really not that smart) figuring out how to approach the templating implementation because I found it baked in quite solid. And again, I absolutely did not want to host a semi-demanding piece of software for my blog, especially on a stack I am not absolutely proficient in. 

So I reluctantly thought about it for a few weeks, trying to push away, but as programming much often does - it pulls you in. So eventually I had to give in from my internal pressure of challenging myself with writing a static site generator.

It needs to be added, that I looked into the [Miniblog.Core](https://github.com/madskristensen/Miniblog.Core/) and I had tried to incorporate my idea around static markdown into a fork of the repo. As I write this now I cannot recall why I left the idea but it was probably because of the my own issue around paying for a runtime and perhaps also something around publishing content. 

Specifically, I wanted to enable my blog to be generated from markdown files, I wanted it to output HTML and be able to adminster that HTML (and CSS, JS) so I could use cheaper hosting and fast serving for readers. And I didn't want to implement any model around any content-persistence, which I mentioned is given to you for free by using a Git repo. Therefore I choose to let the content sit in a Git repo that I could control out-of-sync of the blog, so content and generation is separated.

So here we are. I can now run a command in a console application that creates all the static content I need for my blog, and I can push that around as I wish. I have moved the complexity of generating the blog to my local machine while keeping the possibility to adjust everything I wish, also locally. There is no publishing mechanism, one must find ones own way of moving content to ones hosting provider. 
