window.asisGru = {
    getPreferredTheme: function () {
        return window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
    },
    setTheme: function (theme) {
        document.documentElement.setAttribute('data-theme', theme);
    }
};
