﻿export function showModal(elementID) {
    if (elementID != null && elementID != undefined) {
        let modalElement = document.getElementById(elementID);
        modalElement.style.display = 'block';
        return 'ok';
    }
    else {
        return 'error';
    }
}