window.portfolioSite = {
    getTheme: function () {
        return localStorage.getItem('theme') || 'dark';
    },
    setTheme: function (theme) {
        localStorage.setItem('theme', theme);
        document.documentElement.setAttribute('data-theme', theme);
    },
    registerScrollWatcher: function (dotNetRef) {
        window.addEventListener('scroll', function () {
            dotNetRef.invokeMethodAsync('OnScroll', window.scrollY);
        }, { passive: true });
    }
};
