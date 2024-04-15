Before writing this piece of software I had made a small effort to help out writing a routine
for creating blog comment links, for the blog owned by the Danish Software Quality, Mark Seemann's, at https://blog.ploeh.dk. 

When looking at the repo that serves Marks blog, specifically the notion of how ["Posts" are kept and persisted](https://github.com/ploeh/ploeh.github.com/tree/master/_posts), I thought about how clever it really is to host ones own blog content and at the same time base it of a  Git repository.

> You get a draft-mechanism for free by using Git, because you can choose to not commit and push the posts you keep locally. 

Another great feature of Marks blog is that it is - as far I know - based on the static site generator Jekyll. 

At the same time I was starting to blog again myself, and I had for some period of time used the [mataroa.blog](https://mataroa.blog) which I have been super happy with. It's a no bullshit offering; clean, simple, cheap and I could have kept using it if I did not find the templating options too poor.

I tried for a few days to configure the [Mataroa code](https://github.com/mataroa-blog/mataroa) on my dev-machine and wanted to code a different option for configuring templates, but I found the dependencies too noisy, and I had a hard time figuring out how to approach the templating code because it was pretty "hard" baked in to the system. And I absolutely did not want to 
host a semi-demanding piece of software for my blog.

So I thought about it for a few weeks and eventually had to give in from my internal pressure of challenging myself with writing a static site generator.

Specifically, I wanted to enable my blog to be generated from markdown files, I wanted it to output fully static and controllable HTML so I could cheaper hosting and fast serving for readers, and I didn't want to build any model around any content-persistence. Therefore I choose to let the content sit in a Git repo that I could control out-of-sync of the blog, so content and generation is separated.

So here we are. I can now run a command in a console application that creates all the static content I need for my blog, and I can push that around as I wish. I have moved the complexity of generating the site to my local machine while keeping the possibility to adjust everything I wish, also locally. 