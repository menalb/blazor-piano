window.browserResize = {
    getInnerHeight: function () {        
        return window.innerHeight;
    },
    getInnerWidth: function () {        
        return window.innerWidth;
    },
    registerResizeCallback: function (dotNetObject) {        
        window.addEventListener("resize", () => {
            browserResize.resized(dotNetObject);
        });
    },
    resized: function (obj) {                
        obj.invokeMethodAsync('SetBrowserDimensions', window.innerWidth, window.innerHeight)
            .then(data => data)
            .catch(error => {
                console.log("Error during browser resize: " + error);
            });
    }
}