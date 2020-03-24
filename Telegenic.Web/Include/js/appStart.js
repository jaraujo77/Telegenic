import { formBinder } from "./Admin/formBinder";

document.onreadystatechange = function documentInitialize(e) {
    if (document.readyState === 'interactive') {

        let _route = "";
        let _searchForm = "";
        let _saveForm = "";

        switch (window.location.pathname.toLowerCase()) {

            case "/admin/series":
                _route = "series";
                _searchForm = "search_series";
                _saveForm = "save_series";
                break;
            case "/admin/genre":
                _route = "genre";
                _searchForm = "search_genre";
                _saveForm = "save_genre";
                break;
            case "/admin/movie":
                _route = "movie";
                _searchForm = "search_movie";
                _saveForm = "save_movie";
                break;
            case "/admin/episode":
                _route = "episode";
                _searchForm = "search_episode";
                _saveForm = "save_episode";
                break;
        }

        console.log(_route);
        console.log(_searchForm);
        console.log(_saveForm);
        let _forms = new formBinder(_route, _searchForm, _saveForm);
        _forms.bindInit();
        
    }
}