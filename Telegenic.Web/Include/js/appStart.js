import { seriesForms } from "./Admin/seriesForms";

document.onreadystatechange = function documentInitialize(e) {
    if (document.readyState === 'interactive') {
        let _seriesForms = new seriesForms();
        _seriesForms.bindInit();
    }
}