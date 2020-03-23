class seriesFormsBase {
    constructor() {
        
    }

    addEditEntityHandler(elements) {
        let el = (elements == null || elements == undefined) ? document.querySelectorAll('[data-action=edit]') : elements;

        el.forEach(function (item) {
            item.addEventListener('click', function resultsEditClick(e) {

                e.preventDefault();

                let id = this.getAttribute('data-val');

                let xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function loadSaveForm() {
                    if (this.readyState == 4 && this.status == 200) {
                        let loadForm = new seriesForms();
                        loadForm.resetAllPanels();

                        document.getElementById('SaveFormArea').innerHTML = this.responseText;
                        
                        loadForm.bindResetButton();
                        loadForm.bindSaveButton();
                    }
                }

                xhr.open('get', `/admin/series/save/${id}`, true);
                xhr.send();

            });
        });

    }

    addDeleteEntityHandler(elements) {
        let el = (elements == null || elements == undefined) ? document.querySelectorAll('[data-action=delete]') : elements;

        el.forEach(function (item) {
            
            item.addEventListener('click', function resultsDeleteClick(e) {
                e.preventDefault();

                let id = this.getAttribute('data-val');

                let xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function loadSaveForm() {
                    if (this.readyState == 4 && this.status == 200) {

                        let loadForm = new seriesForms();
                        loadForm.resetAllPanels();

                        document.getElementById('SaveFormArea').innerHTML = this.responseText;
                        
                        loadForm.bindDeleteButton();
                    }
                }

                xhr.open('post', `/admin/series/delete/${id}`, true);
                xhr.send();
            });
        });
    }

    addSearchEntitiesHandler() {
        let el = document.getElementById('btnSearch');
        
        el.addEventListener('click', function executeSearch(e) {
            e.preventDefault();

            let form = document.forms['search_series'];
            let formData = new FormData(form);

            let xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function loadSaveForm() {
                if (this.readyState == 4 && this.status == 200) {

                    let loadForm = new seriesForms();
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
        el.forEach(function (item) {
            
            item.addEventListener('click', function resetPage(e) {
                e.preventDefault();

                let form = document.forms['search_series'];
                form.reset();

                let loadForm = new seriesForms();
                loadForm.resetAllPanels();
            });
        });
    }

    addSaveEntityHandler() {
        let el = document.querySelectorAll('[data-action=save]');

        el.forEach(function (item) {
            
            item.addEventListener('click', function saveEntity(e) {
                e.preventDefault();
                let form = document.forms['save_series'];
                let formData = new FormData(form);

                console.log("ready to post form");
                console.log(formData);

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

        el.forEach(function (item) {
            item.addEventListener('click', function resultsAddClick(e) {

                e.preventDefault();

                let id = 0;

                let xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function loadSaveForm() {
                    if (this.readyState == 4 && this.status == 200) {
                        let loadForm = new seriesForms();
                        loadForm.resetAllPanels();

                        document.getElementById('SaveFormArea').innerHTML = this.responseText;
                        
                        loadForm.bindResetButton();
                        loadForm.bindSaveButton();
                    }
                }

                xhr.open('get', `/admin/singleseries/save/${id}`, true);
                xhr.send();

            });
        });
    }

    static resetAllPanels() {
        seriesFormsBase.resetFormPanel();
        seriesFormsBase.resetsearchGridPanel();
    }

    static resetsearchGridPanel() {
        document.getElementById('GridResultArea').innerHTML = '';
    }

    static resetFormPanel() {
        document.getElementById('SaveFormArea').innerHTML = '';
    }

}

export class seriesForms extends seriesFormsBase {
    constructor() {
        super();
    }

    bindEditButton() {
        super.addEditEntityHandler();
    }

    bindDeleteButton() {
        super.addDeleteEntityHandler();
    }

    bindSaveButton() {
        super.addSaveEntityHandler();
    }

    bindSearchButton() {
        super.addSearchEntitiesHandler();
    }

    bindResetButton() {
        super.addResetPageHandler();
    }

    bindAddButton() {
        super.addAddEntityHander();
    }

    resetAllPanels() {
        seriesForms.resetAllPanels();
    }

    bindInit() {
        this.bindEditButton();
        this.bindDeleteButton();
        this.bindSaveButton();
        this.bindSearchButton();
        this.bindResetButton();
        this.bindAddButton();
    }
}