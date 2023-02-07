
// This is a script that was used to migrate media content to Azure storage.
// To use it, log in to the Sitefinity backend and navigate to Content > Images.
// Paste and run the script in the javascript console.
// A button labeled "Relocate Images" will be added to the toolbar under the "Upload Images" button.
// This has not been tested in the new Sitefinity admin UI.
// A list of sites and providers is configurable below.
// The browser tab needs to stay open while the script migrates each library individually.
// Check the console log for output messages.

(function () {
    var storage = "Azure%20Storage"

    var sites = [
        //{ site: "7400656C-9C85-4BD8-B62F-433110FDD7BB", provider: "librariesProvider2" }, 
        //{ site: "0852AE88-EAC7-4837-93A8-50A27AE3C318", provider: "librariesProvider3" }, 
        //{ site: "F9B2D7F3-B148-4D89-B1E1-3BF71FE4AFE1", provider: "librariesProvider4" }, 
        //{ site: "623DF1B4-B494-4A53-AFC3-AB5FFEA81ED0", provider: "librariesProvider5" }, 
        //{ site: "70B367F4-A9DC-48BB-B65D-A6BD98533C85", provider: "librariesProvider6" }, 
        //{ site: "2BAE0520-4DEB-455B-ACD1-01C45F1C7279", provider: "librariesProvider7" }, 
        //{ site: "C46493EF-782A-4D28-A426-A1BD28036F3B", provider: "librariesProvider8" }, 
        //{ site: "5E4DB9FC-6ABF-4E51-88E4-4A39F5117AA3", provider: "librariesProvider9" }, 
        //{ site: "161E8589-7D88-455B-A6BA-5634AD0A62EC", provider: "librariesProvider10" }, 
        //{ site: "AEB184BB-95E3-4817-AC8B-6CECE7DFF606", provider: "librariesProvider11" }, 
        //{ site: "F65C6593-1359-4D83-A88D-81F49DF98D7B", provider: "OpenAccessDataProvider" }, 
        //{ site: "9752B95C-B15A-4A51-B264-99B3D387D35B", provider: "librariesProvider12" }, 
    ];

    function createButton() {
        var a = document.createElement('a');
        var linkText = document.createTextNode("Relocate Images");
        a.appendChild(linkText);
        a.title = "Relocate";
        a.style = "margin-top: 20px;";
        a.className = "sfLinkBtn";
        a.onclick = loopSites;
        document.getElementById("imgAlbmsCntView_albumsBackendList_ctl00_ctl00_toolbar").appendChild(a);
    }

    async function loopSites() {
        for (let i = 0; i < sites.length; i++) {
            let site = sites[i].site
            let provider = sites[i].provider

            console.log(`Processing site ${site}, provider ${provider}`)

            await processLibrary(site, provider, "Telerik.Sitefinity.Libraries.Model.VideoLibrary")
            await processLibrary(site, provider, "Telerik.Sitefinity.Libraries.Model.DocumentLibrary")
            await processLibrary(site, provider, "Telerik.Sitefinity.Libraries.Model.Album")

            console.log(`Done with site ${site}, provider ${provider}`)
        }

        console.log(`Content migration finished`)
    }

    async function processLibrary(site, provider, type) {
        let ids = await getLibraryIds(site, provider, type)

        console.log(`Found ${ids.length} libraries for site ${site}, provider ${provider}, type ${type}`)

        for (let j = 0; j < ids.length; j++) {
            console.log(ids[j]);
            relocateLibrary(site, provider, type, ids[j]);
            await sleep(20 * 1000);
        }

        console.log(`Done with site ${site}, provider ${provider}, type ${type}`)
    }

    async function getLibraryIds(site, provider, type) {
        let service = "none.svc"

        switch (type) {
            case "Telerik.Sitefinity.Libraries.Model.VideoLibrary":
                service = "VideoLibraryService.svc"
                break;
            case "Telerik.Sitefinity.Libraries.Model.DocumentLibrary":
                service = "DocumentLibraryService.svc"
                break;
            case "Telerik.Sitefinity.Libraries.Model.Album":
                service = "AlbumService.svc"
                break;
        }

        return fetch("https://site.org/Sitefinity/Services/Content/" + service + "/?managerType=&providerName=&itemType=" + type + "&provider=" + provider + "&sortExpression=Title%20ASC&skip=0&take=100&sf_site=" + site, {
            "headers": {
                "accept": "*/*",
                "accept-language": "en-US,en;q=0.9",
                "sec-ch-ua": "\".Not/A)Brand\";v=\"99\", \"Google Chrome\";v=\"103\", \"Chromium\";v=\"103\"",
                "sec-ch-ua-mobile": "?0",
                "sec-ch-ua-platform": "\"Windows\"",
                "sec-fetch-dest": "empty",
                "sec-fetch-mode": "cors",
                "sec-fetch-site": "same-origin",
                "sf_site": site,
                "x-requested-with": "XMLHttpRequest"
            },
            "referrer": "https://site.org/Sitefinity/Content/Images?sf_site=" + site,
            "referrerPolicy": "strict-origin-when-cross-origin",
            "body": null,
            "method": "GET",
            "mode": "cors",
            "credentials": "include"
        })
            .then(x => x.json())
            .then(x => x.Items.map(y => y.Id))
    }

    function relocateLibrary(site, provider, type, contentId) {
        if (!contentId) {
            console.warn(`Received ${contentId}`)
            return;
        }

        fetch("https://site.org/Sitefinity/Services/Content/LibraryRelocationService.svc/relocate/?libraryType=" + type + "&libraryProvider=" + provider + "&libraryId=" + contentId + "&blobProvider=" + storage, {
            "headers": {
                "accept": "*/*",
                "accept-language": "en-US,en;q=0.9",
                "content-type": "application/json",
            },
            "referrer": "https://site.org/Sitefinity/Dialog/LibraryRelocateDialog?mode=TransferLibrary&provider=" + provider + "&sf_site=" + site,
            "referrerPolicy": "strict-origin-when-cross-origin",
            "body": null,
            "method": "PUT",
            "mode": "cors",
            "credentials": "include"
        })
            .then(x => console.log(`Done with ${contentId}`))
    }

    function sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    createButton()
})();
