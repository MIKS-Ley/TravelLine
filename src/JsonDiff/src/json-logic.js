import { JsonDiff } from "./json-diff.js";

const form = document.querySelector(`.main-form`);
const textareaOld = document.querySelector(`#oldJson`);
const textareaNew = document.querySelector(`#newJson`);
const oldJsonError = document.querySelector(`#oldJson-error`);
const newJsonError = document.querySelector(`#newJson-error`);
const resultJsonBlock = document.querySelector(`.result-json`);
const button = document.querySelector(`.main-form button`);

function validateJSON(textarea, errorElement) {
    const value = textarea.value.trim();
    errorElement.textContent = '';

    if (!value) {
        errorElement.textContent = 'Обязательное поле';
        return false;
    }

    try {
        JSON.parse(value);
        return true;
    } catch (e) {
        errorElement.textContent = 'Некорректный JSON';
        return false;
    }
}

button.addEventListener(`click`, async (event) => {
    event.preventDefault();

    const isOldValid = validateJSON(textareaOld, oldJsonError);
    const isNewValid = validateJSON(textareaNew, newJsonError);

    if (!isOldValid || !isNewValid) {
        resultJsonBlock.innerHTML = '';
        resultJsonBlock.style.display = 'none';
        return;
    }

    const originalButtonText = button.textContent;
    button.textContent = `Loading...`;
    button.disabled = true;
    resultJsonBlock.innerHTML = '';
    resultJsonBlock.style.display = 'block';

    try {
        const oldValue = JSON.parse(textareaOld.value);
        const newValue = JSON.parse(textareaNew.value);

        const result = await JsonDiff.create(oldValue, newValue);
        const jsonResult = JSON.stringify(result, null, 2);

        resultJsonBlock.innerHTML = `<pre>${jsonResult}</pre>`;
    } catch (error) {
        console.error("Error:", error);
        resultJsonBlock.innerHTML = `<div class="error">Произошла ошибка при сравнении JSON</div>`;
    } finally {
        button.textContent = originalButtonText;
        button.disabled = false;
    }
});