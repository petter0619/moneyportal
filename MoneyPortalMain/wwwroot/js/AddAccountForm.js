const addAccountFormPopup = document.querySelector('.addAccountPopup');
const addAccountFormPopupTriggers = document.querySelectorAll('.addAccountPopup__trigger');

const handleShowPopup = e => {
    const isOpen = addAccountFormPopup.classList.contains('addAccountPopup--open');

    if (isOpen) {
        addAccountFormPopup.classList.remove('addAccountPopup--open');
    } else {
        addAccountFormPopup.classList.add('addAccountPopup--open');
    }
}


addAccountFormPopupTriggers.forEach(trigger => trigger.addEventListener('click', handleShowPopup));