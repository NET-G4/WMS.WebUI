function logout() {
    sessionStorage.clear();
    localStorage.clear();

    window.location.href = '/account/login';

    setTimeout(function () {
        window.history.replaceState(null, null, window.location.href);
        window.history.pushState(null, null, window.location.href);
    }, 0);
}