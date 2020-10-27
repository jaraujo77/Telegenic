import { formBinder } from "./formBinder";


export class formBase {
    
    setBtnEditClickHandler(elements) {
        let el = (elements == null || elements == undefined) ? document.querySelectorAll('[data-action=edit]') : elements;

        el.forEach(function (item) {

            item.addEventListener('click', function resultsEditClick(e) {
                e.preventDefault();
            
                let id = this.getAttribute('data-val');
                let route = this.getAttribute('href');

                let xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function loadSaveForm() {
                    if (this.readyState == 4 && this.status == 200) {
                        
                        formBase.resetAllPanels();

                        document.getElementById('EditFormArea').innerHTML = this.responseText;

                        let binder = new formBinder();
                        binder.bindInit();
                    }
                }

                xhr.open('get', `${route}`, true);
                xhr.send();
            });
        });

    }

    setBtnDeleteClickHandler(elements) {
        let el = (elements == null || elements == undefined) ? document.querySelectorAll('[data-action=delete]') : elements;        

        el.forEach(function (item) {

            item.addEventListener('click', function resultsDeleteClick(e) {
                e.preventDefault();

                let id = this.getAttribute('data-val');
                let route = this.getAttribute('href');

                let xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function loadSaveForm() {
                    if (this.readyState == 4 && this.status == 200) {

                        let binder = new formBinder();
                        binder.resetAllPanels();

                        document.getElementById('EditFormArea').innerHTML = this.responseText;

                        binder.bindInit();
                    }
                }

                xhr.open('post', `${route}`, true);
                xhr.send();
            });
        });
    }

    setBtnSearchClickHandler() {
        let el = document.querySelectorAll('#btnSearch');
        let base = this;

        el.forEach(function setSearchClickHandler(item) {
            item.addEventListener('click', function btnSearchClick(e) {
                e.preventDefault();

                let form = item.form;
                let formData = new FormData(form);

                let xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function loadResults(e) {
                    if (this.readyState == 4 && this.status == 200) {
                        let binder = new formBinder();
                        binder.resetAllPanels();
                        document.getElementById('GridResultArea').innerHTML = this.responseText;

                        binder.bindEditButton();
                        binder.bindDeleteButton();
                    }
                }

                xhr.open('post', `${form.action}`, true);
                xhr.send(formData);
            });
        });
    }

    setBtnResetClickHandler() {
        let el = document.querySelectorAll('[id=btnReset]');
        el.forEach(function (item) {
            item.addEventListener('click', function resetPage(e) {
                e.preventDefault();

                let form = this.form;
                form.reset();

                formBase.resetAllPanels();
                console.log("I'm in your base, resetting your dudes!");
            });
        });
    }
    
    setBtnSaveClickHandler() {
        let el = document.querySelectorAll('[id=btnSubmit]');

        el.forEach(function (item) {

            item.addEventListener('click', function saveEntity(e) {
                e.preventDefault();

                let form = this.form;
                let formData = new FormData(form);
                
                let xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function loadSaveForm() {
                    if (this.readyState == 4 && this.status == 200) {
                        let binder = new formBinder();

                        document.getElementById('EditFormArea').innerHTML = this.responseText;
                        
                        binder.bindInit();
                    }
                }

                xhr.open('post', `${form.action}`, true);
                xhr.send(formData);
            });


        });
    }

    setBtnAddClickHandler(elements) {
        let el = (elements == null || elements == undefined) ? document.querySelectorAll('[id=btnAdd]') : elements;
        

        el.forEach(function (item) {
            item.addEventListener('click', function resultsAddClick(e) {

                e.preventDefault();
                                
                let route = this.getAttribute('href');

                let xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function loadSaveForm() {
                    if (this.readyState == 4 && this.status == 200) {
                        let binder = new formBinder();
                        binder.resetAllPanels();

                        document.getElementById('EditFormArea').innerHTML = this.responseText;

                        binder.bindInit();
                        console.log("I'm in your base, binding your dudes!");
                    }
                }

                xhr.open('get', `${route}`, true);
                xhr.send();

            });
        });
    }

    static resetAllPanels() {
        formBase.resetFormPanel();
        formBase.resetsearchGridPanel();
    }

    static resetsearchGridPanel() {
        document.getElementById('GridResultArea').innerHTML = '';
    }

    static resetFormPanel() {
        document.getElementById('EditFormArea').innerHTML = '';
    }
}