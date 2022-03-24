const sidebarTrigger = document.querySelector('.sidebar__trigger');
let showMenu = false;

function toggleMenu() {
    const hamburger = document.querySelector('.sidebar__burger');
    const sidebar = document.querySelector('.sidebar');

    if (!showMenu) {
        hamburger.classList.add('open');
        sidebar.style.transition = "all 0.3s ease-in-out";
        sidebar.classList.add('open');

        showMenu = true;
    } else {
        hamburger.classList.remove('open');
        sidebar.classList.remove('open');

        setTimeout(function () {
            nav.style.transition = "";
        }, 300);

        showMenu = false;
    }
}

sidebarTrigger.addEventListener('click', toggleMenu);