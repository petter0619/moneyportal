const menuBtn = document.querySelector('.menu-btn');
const hamburger = document.querySelector('.menu-btn__burger');
const nav = document.querySelector('nav');
let showMenu = false;

function toggleMenu() {
    if (!showMenu) {
        hamburger.classList.add('open');
        nav.style.transition = "all 0.3s ease-in-out";
        nav.classList.add('open');

        showMenu = true;
    } else {
        hamburger.classList.remove('open');
        nav.classList.remove('open');

        setTimeout(function () {
            nav.style.transition = "";
        }, 300);

        showMenu = false;

    }
}

menuBtn.addEventListener('click', toggleMenu);