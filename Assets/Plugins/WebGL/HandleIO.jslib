var HandleIO = {
    WindowAlert : function(message)
    {
        window.alert(Pointer_stringify(message));
    },
    SyncFiles : function()
    {
        FS.syncfs(false,function (err) {
            // handle callback
        });
    },
    ConsoleLog: function(message)
    {
        console.log(message);
    }
};

mergeInto(LibraryManager.library, HandleIO);