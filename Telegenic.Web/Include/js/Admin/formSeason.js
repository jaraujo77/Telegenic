export class formSeason {

    addBtnEditClickHandler() {
        let el = document.querySelectorAll("#btnEdit");

        el.forEach(function setSeasonEditClickHandler(item) {
            item.addEventListener('click', function setEditClickHandler(e) {
                e.preventDefault();
                console.log("clicked Season Edit Button!");
                console.log("Route to get: " + el.pathname);

                let xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function loadSeasonEditForm() {
                    if (this.readyState == 4 && this.status == 200) {
                        document.getElementById('tg-season-panel').innerHTML = this.responseText;
                        let season = new formSeason();
                        season.addBtnSeasonSubmitClickHander();
                        season.addBtnSeasonCancelClickHandler();
                    }
                }

                xhr.open('get', item.pathname, true);
                xhr.send();
            });
        });
    }

    addBtnSeasonSubmitClickHander() {
        let el = document.getElementById('btnSeasonSubmit');
        let base = this;

        el.addEventListener('click', function saveSeason(e) {
            e.preventDefault();
            let form = document.forms['save_season'];
            let formData = new FormData(form);

            let xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function loadSeasonDetail() {
                if (this.readyState == 4 && this.status == 200) {
                    document.getElementById('tg-season-panel').innerHTML = this.responseText;
                    let season = new formSeason();
                    season.addBtnEditClickHandler();
                }
            }

            xhr.open('post', `${form.action}`, true);
            xhr.send(formData);
            console.log("clicked season save button!");
        });
    }

    addBtnSeasonCancelClickHandler() {
        let el = document.getElementById('btnSeasonCancel');

        el.addEventListener('click', function cancelSeasonEditForm(e) {
            e.preventDefault();

            let xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function loadSeasonEditForm() {
                if (this.readyState == 4 && this.status == 200) {
                    document.getElementById('tg-season-panel').innerHTML = this.responseText;
                    let season = new formSeason();
                    season.addBtnEditClickHandler();
                }
            }

            xhr.open('get', el.pathname, true);
            xhr.send();
        });
    }
}