[//]: # "title: hugga bugga ulla johnson"
[//]: # "slug: hugga-bugga-ulla-johnson"
[//]: # "pubDate: 14/12/2023 12:01"
[//]: # "lastModified: 14/12/2023 13:07"
[//]: # "excerpt: This is really interesting stuff!!!"
[//]: # "categories: code, acr, azure, easypeasy, iaas"
[//]: # "isPublished: true"

I use the command line tools Azure CLI, inside PowerShell.

First we must login

```cmd
az login
```

Then we can go right ahead and create an ACR. If you do not have a resource you wish to use you can go ahead and creata a new one.

```cmd
az acr create --resource-group frostfyi --name frostfyiacr --sku Basic
```

I am also going to enable the admin user access for the ACR.

```cmd
az acr update -n frostfyiacr --admin-enabled true
```

That's it. 

You can now login to azure.com and find you newly created ACR. 

Easy money.

Let's use the az cli to see the ACR.

```cmd
az acr show -n frostfyiacr
```

It works.

I have a service which has a docker file. Let's package the service into an docker image and push it to our ACR.

It's important to note that your image must be tagged correctly for being pushed to the registry.

We tag it with registry name frostfyiacr.azurecr.io. And repository frostrssweb. And version v1.

```cmd
docker build -t frostfyiacr.azurecr.io/frostrssweb:v1 .
```

You can check with docker (I use Docker for Windows) if the image is correctly named.

```cmd
docker images
```

Now let's login to the ACR. And then push the image.

```cmd
az acr login

docker push frostfyiacr.azurecr.io/frostrssweb:v1
```

That's it. We have successfully created a Azure Container Register and pushed a image to it.