const dropdownTriggers = document.querySelectorAll('.sidebar__dropdown-trigger');
const sidebarTrigger = document.querySelector('.sidebar__trigger');
let showMenu = false;

const handleDropdownClick = e => {
    const dropdownButton = e.currentTarget;
    const dropdownList = dropdownButton.parentNode.querySelector('.sidebar__dropdown');

    const dropdownIsOpen = dropdownButton.classList.contains('sidebar__dropdown-trigger--open');

    if (dropdownIsOpen) {
        dropdownButton.classList.remove('sidebar__dropdown-trigger--open');
        dropdownList.style.height = '0';
    } else {
        dropdownButton.classList.add('sidebar__dropdown-trigger--open');

        const height = Array.from(dropdownList.children).reduce((height, li) => {
            return height += li.offsetHeight;
        }, 0);

        dropdownList.style.height = `${height}px`;
    }
}

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
            sidebar.style.transition = "";
        }, 300);

        showMenu = false;
    }
}

dropdownTriggers.forEach(trigger => trigger.addEventListener('click', handleDropdownClick));
sidebarTrigger.addEventListener('click', toggleMenu);