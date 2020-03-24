import { formBinder } from "./formBinder";

export class formBase {
    constructor(route, searchFormName, saveFormName) {
        this.route = route;
        this.searchFormName = searchFormName;
        this.saveFormName = saveFormName;        
    }

    addEditEntityHandler(elements) {
        let el = (elements == null || elements == undefined) ? document.querySelectorAll('[data-action=edit]') : elements;
        let base = this;

        el.forEach(function (item) {

            item.addEventListener('click', function resultsEditClick(e) {
                e.preventDefault();

                console.log("base save form name: " + base.saveFormName);
            
                let id = this.getAttribute('data-val');

                let xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function loadSaveForm() {
                    if (this.readyState == 4 && this.status == 200) {
                        let loadForm = new formBinder(base.route, base.searchFormName, base.saveFormName);
                        loadForm.resetAllPanels();

                        document.getElementById('SaveFormArea').innerHTML = this.responseText;

                        loadForm.bindResetButton();
                        loadForm.bindSaveButton();
                    }
                }

                xhr.open('get', `/admin/${base.route}/save/${id}`, true);
                xhr.send();

            });
        });

    }

    addDeleteEntityHandler(elements) {
        let el = (elements == null || elements == undefined) ? document.querySelectorAll('[data-action=delete]') : elements;
        let base = this;

        el.forEach(function (item) {

            item.addEventListener('click', function resultsDeleteClick(e) {
                e.preventDefault();

                let id = this.getAttribute('data-val');

                let xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function loadSaveForm() {
                    if (this.readyState == 4 && this.status == 200) {

                        let loadForm = new formBinder(base.route, base.searchFormName, base.saveFormName);
                        loadForm.resetAllPanels();

                        document.getElementById('SaveFormArea').innerHTML = this.responseText;

                        loadForm.bindEditButton();
                        loadForm.bindDeleteButton();
                    }
                }

                xhr.open('post', `/admin/${base.route}/delete/${id}`, true);
                xhr.send();
            });
        });
    }

    addSearchEntitiesHandler() {
        let el = document.getElementById('btnSearch');
        let base = this;

        el.addEventListener('click', function executeSearch(e) {
            e.preventDefault();            
            
            let form = document.forms[`${base.searchFormName}`];
            let formData = new FormData(form);

            let xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function loadSaveForm(e) {
                if (this.readyState == 4 && this.status == 200) {

                    let loadForm = new formBinder(base.route, base.searchFormName, base.saveFormName);
                    loadForm.resetAllPanels();

                    document.getElementById('GridResultArea').innerHTML = this.responseText;

                    loadForm.bindEditButton();
                    loadForm.bindDeleteButton();
                }
            }

            xhr.open('post', `${form.action}`, true);
            xhr.send(formData);
        });
    }

    addResetPageHandler() {
        let el = document.querySelectorAll('[id=btnReset]');
        let base = this;
        el.forEach(function (item) {

            item.addEventListener('click', function resetPage(e) {
                e.preventDefault();

                let form = document.forms[`${base.searchFormName}`];
                form.reset();

                let loadForm = new formBinder(base.route, base.searchFormName, base.saveFormName);
                loadForm.resetAllPanels();
            });
        });
    }

    addSaveEntityHandler() {
        let el = document.querySelectorAll('[data-action=save]');
        let base = this;

        el.forEach(function (item) {

            item.addEventListener('click', function saveEntity(e) {
                e.preventDefault();
                let form = document.forms[`${base.saveFormName}`];
                let formData = new FormData(form);
                
                let xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function loadSaveForm() {
                    if (this.readyState == 4 && this.status == 200) {
                        document.getElementById('SaveFormArea').innerHTML = this.responseText;
                    }
                }

                xhr.open('post', `${form.action}`, true);
                xhr.send(formData);

            });


        });
    }

    addAddEntityHander(elements) {
        let el = (elements == null || elements == undefined) ? document.querySelectorAll('[data-action=add]') : elements;
        let base = this;

        el.forEach(function (item) {
            item.addEventListener('click', function resultsAddClick(e) {

                e.preventDefault();

                let id = 0;

                let xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function loadSaveForm() {
                    if (this.readyState == 4 && this.status == 200) {
                        let loadForm = new formBinder(base.route, base.searchFormName, base.saveFormName);
                        loadForm.resetAllPanels();

                        document.getElementById('SaveFormArea').innerHTML = this.responseText;

                        loadForm.bindResetButton();
                        loadForm.bindSaveButton();
                    }
                }

                xhr.open('get', `/admin/${base.route}/save/${id}`, true);
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
        document.getElementById('SaveFormArea').innerHTML = '';
    }
}