document.addEventListener('DOMContentLoaded', () => {
    const buttonLogin = document.getElementById('buttonLogin');
    const buttonStart = document.getElementById('buttonStart');
    const homePage = document.getElementById('homePage');
    const loginPage = document.getElementById('loginPage');
    const startPage = document.getElementById('startPage');
    const loginForm = document.getElementById('loginForm');
    const loginInput = document.getElementById('login');
    const errorElement = document.querySelector('.error');
    const authorization = document.querySelector('.authorization');
    const userName = document.getElementById('userName');

    let currentUser = null;

    function init() {
        const savedUser = localStorage.getItem('currentUser');
        if (savedUser) {
            currentUser = savedUser;
            updateUI();
        }
    }

    function showHomePage() {
        hideAllSections();
        homePage.hidden = false;
    }

    function showLoginPage() {
        hideAllSections();
        loginPage.hidden = false;
    }

    function showStartPage() {
        hideAllSections();
        startPage.hidden = false;
    }

    function hideAllSections() {
        document.querySelectorAll('main > section').forEach(section => {
            section.hidden = true;
        });
    }

    function updateUI() {
        if (currentUser) {
            userName.textContent = currentUser;
            authorization.style.display = 'inline';
            buttonLogin.textContent = 'Log out';
            buttonStart.hidden = false;
            if (startPage.hidden) showHomePage();
        } else {
            userName.textContent = '';
            authorization.style.display = 'none';
            buttonLogin.textContent = 'Log in';
            buttonStart.hidden = true;
            showHomePage();
        }
    }

    buttonLogin.addEventListener('click', (e) => {
        e.preventDefault();

        if (currentUser) {
            currentUser = null;
            localStorage.removeItem('currentUser');
            updateUI();
        } else {
            showLoginPage();
        }
    });

    buttonStart.addEventListener('click', (e) => {
        e.preventDefault();
        showStartPage();
    });

    loginForm.addEventListener('submit', (e) => {
        e.preventDefault();

        const username = loginInput.value.trim();
        if (!username) {
            showError('Обязательное поле');
            return;
        }

        currentUser = username;
        localStorage.setItem('currentUser', currentUser);
        resetLoginForm();
        updateUI();
    });

    function showError(message) {
        errorElement.textContent = message;
        errorElement.classList.add('error--visible');
        loginInput.classList.add('input--error');
        loginInput.focus();
    }

    function resetLoginForm() {
        loginInput.value = '';
        errorElement.classList.remove('error--visible');
        loginInput.classList.remove('input--error');
        errorElement.textContent = '';
    }

    loginInput.addEventListener('input', () => {
        if (errorElement.textContent) {
            errorElement.classList.remove('error--visible');
            loginInput.classList.remove('input--error');
            errorElement.textContent = '';
        }
    });

    init();
});