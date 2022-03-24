const dropdownTriggers = document.querySelectorAll('.sidebar__dropdown-trigger');

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

dropdownTriggers.forEach(trigger => trigger.addEventListener('click', handleDropdownClick));