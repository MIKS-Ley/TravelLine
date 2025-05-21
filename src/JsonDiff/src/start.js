document.addEventListener('DOMContentLoaded', () => {
    const logo = document.querySelector('.logo');
    const buttonStart = document.getElementById('buttonStart');
    const homePage = document.getElementById('homePage');
    const loginPage = document.getElementById('loginPage');
    const startPage = document.getElementById('startPage');

    let currentUser = localStorage.getItem('currentUser') || null;

    function updateUI() {
        if (currentUser) {
            if (buttonStart) buttonStart.hidden = false;
        } else {
            if (buttonStart) buttonStart.hidden = true;
        }
    }

    function hideAllSections() {
        [homePage, loginPage, startPage].forEach(section => {
            if (section) section.hidden = true;
        });
    }

    if (logo) {
        logo.addEventListener('click', (e) => {
            e.preventDefault();
            hideAllSections();
            if (homePage) homePage.hidden = false;
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
        });
    }

    if (buttonStart) {
        buttonStart.addEventListener('click', (e) => {
            e.preventDefault();
            hideAllSections();
            if (startPage) startPage.hidden = false;
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
        });
    }

    updateUI();
});