var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var gameTypeScript;
(function (gameTypeScript) {
    let btnDice;
    let btnCard;
    let status;
    let diceImg;
    let cardImg;
    const lstCards = [
        { name: "Facebook", value: 5 },
        { name: "Instagram", value: 5 },
        { name: "Twitter", value: 5 },
        { name: "Tiktok", value: 5 },
        { name: "Netflix", value: 5 },
        { name: "Twitch", value: 5 },
        { name: "Vimeo", value: 5 },
        { name: "Whatsapp", value: 4 },
        { name: "Linkedin", value: 4 },
        { name: "YouTube", value: 3 },
        { name: "Roblox", value: 3 },
        { name: "Skype", value: 3 },
        { name: "Steam", value: 3 },
        { name: "Telegram", value: 3 },
        { name: "Pinterest", value: 3 },
        { name: "Googleplay", value: 3 },
        { name: "Zoom", value: 3 },
        { name: "Bing", value: 2 },
        { name: "Google", value: 2 },
        { name: "Bbcnews", value: 2 },
        { name: "Nytimes", value: 2 },
        { name: "Gmail", value: 1 },
        { name: "Slack", value: 1 },
        { name: "Yahoomail", value: 1 },
        { name: "Outlook", value: 1 },
        { name: "Aolmail", value: 1 },
        { name: "Hashomrim1", value: 5 },
        { name: "Hashomrim2", value: 5 },
        { name: "Hashomrim3", value: 5 },
        { name: "Hashomrim4", value: 5 },
        { name: "Hashomrim5", value: 5 },
        { name: "Hashomrim6", value: 5 },
        { name: "Hashomrim7", value: 4 },
        { name: "Hashomrim8", value: 4 },
        { name: "Hashomrim9", value: 3 },
        { name: "Hashomrim10", value: 3 },
        { name: "Hashomrim11", value: 3 },
        { name: "Hashomrim12", value: 3 },
        { name: "Hashomrim13", value: 3 },
        { name: "Hashomrim14", value: 3 },
        { name: "Hashomrim15", value: 3 },
        { name: "Hashomrim16", value: 3 },
        { name: "Hashomrim17", value: 2 },
        { name: "Hashomrim18", value: 2 },
        { name: "Hashomrim19", value: 2 },
        { name: "Hashomrim20", value: 2 },
        { name: "Hashomrim21", value: 1 },
        { name: "Hashomrim22", value: 1 },
        { name: "Hashomrim23", value: 1 },
        { name: "Hashomrim24", value: 1 },
        { name: "Hashomrim25", value: 1 },
        { name: "Hashomrim26", value: 1 }
    ];
    const player1Value = "fa-heart";
    const player1 = '<i class="fa-solid fa-heart"></i>';
    let player2 = '<i class="fa-solid fa-dragon"></i>';
    let player2Value = "fa-dragon";
    let gameStatus;
    (function (gameStatus) {
        gameStatus[gameStatus["NotStarted"] = 0] = "NotStarted";
        gameStatus[gameStatus["Playing"] = 1] = "Playing";
        gameStatus[gameStatus["Winner"] = 2] = "Winner";
        gameStatus[gameStatus["Loser"] = 3] = "Loser";
    })(gameStatus || (gameStatus = {}));
    let currentStatus;
    let currentPlayerSpot;
    let currentPlayerValue;
    let currentPlayer;
    let winner = "";
    let loser = "";
    let diceValue;
    let cardValue;
    let cardOnly;
    let playComputer;
    const delay = (ms) => new Promise(res => setTimeout(res, ms));
    $(document).ready(function () {
        btnDice = $("#btnDice").click(throwDice);
        btnCard = $("#btnCard").click(chooseCard);
        status = document.querySelector("#gameStatus");
        diceImg = document.querySelector("#diceImg");
        cardImg = document.querySelector("#cardImg");
        setupGame();
        setBtnText();
        document.querySelector("#btnRestart").addEventListener("click", setupGame);
        document.querySelector("#btnStart").addEventListener("click", startGame);
    });
    function setBtnText() {
        const btn = document.querySelector("#btnRestart");
        switch (currentStatus) {
            case gameStatus.Playing:
                btn.textContent = "Restart";
                btn.classList.replace("bg-primary", "bg-warning");
                break;
            default:
                btn.textContent = "Start";
                btn.classList.replace("bg-warning", "bg-primary");
                break;
        }
    }
    function setupGame() {
        document.querySelector("#gameSettings").classList.remove("d-none");
        document.querySelectorAll(".front").forEach(s => s.innerHTML = "");
        btnDice.prop("disabled", true);
        btnCard.prop("disabled", true);
        currentStatus = gameStatus.NotStarted;
        setStatusLbl();
        diceImg.src = "";
        cardImg.src = "";
        diceValue = 0;
        cardValue = "";
        btnDice.removeClass("d-none");
        btnCard.parent().addClass("col-6");
    }
    function startGame() {
        document.querySelector("#gameSettings").classList.add("d-none");
        cardOnly = document.querySelector("#rdCardOnly").checked;
        playComputer = document.querySelector("#rdPlayComputer").checked;
        let playerSpot = document.querySelector("#spot50");
        if (playComputer) {
            player2 = player2.replace("dragon", "computer");
            player2Value = "fa-computer";
        }
        else {
            player2 = player2.replace("computer", "dragon");
        }
        document.querySelectorAll(".front").forEach(s => s.textContent = "");
        playerSpot.innerHTML += player1;
        playerSpot.innerHTML += player2;
        currentPlayerSpot = $(playerSpot);
        currentPlayerValue = player1Value;
        currentPlayer = player1;
        currentStatus = gameStatus.Playing;
        btnDice.prop("disabled", false);
        btnCard.prop("disabled", !cardOnly);
        if (cardOnly) {
            btnDice.addClass("d-none");
            btnCard.parent().removeClass("col-6");
        }
        setStatusLbl();
        setBtnText();
    }
    function takeTurn() {
        return __awaiter(this, void 0, void 0, function* () {
            if (currentStatus == gameStatus.Playing) {
                let currentSpot = parseInt(currentPlayerSpot.attr('id').substring(4));
                let originalSpot = parseInt(currentPlayerSpot.attr('id').substring(4));
                if (cardValue.includes("Hashomrim")) {
                    if (currentSpot == 100) {
                        currentStatus = gameStatus.Winner;
                        winner = currentPlayer;
                        setStatusLbl();
                        return;
                    }
                    currentSpot += diceValue;
                }
                else {
                    if (currentSpot == 0) {
                        currentStatus = gameStatus.Loser;
                        loser = currentPlayer;
                        setStatusLbl();
                        return;
                    }
                    currentSpot -= diceValue;
                }
                if (currentSpot > 100) {
                    currentSpot = 100;
                }
                else if (currentSpot < 0) {
                    currentSpot = 0;
                }
                for (let i = originalSpot; i != (originalSpot > currentSpot ? currentSpot - 1 : currentSpot + 1); i += originalSpot < currentSpot ? 1 : -1) {
                    let iconToMove = currentPlayerSpot.find(`.${currentPlayerValue}`);
                    let newSpot = $(`#spot${i}`);
                    currentPlayerSpot = newSpot;
                    iconToMove.slideDown(500);
                    yield delay(500);
                    newSpot.append(iconToMove);
                    yield delay(500);
                    if (i !== currentSpot) {
                        iconToMove.slideUp(500);
                    }
                }
                switchPlayer();
                btnDice.prop("disabled", false);
                btnCard.prop("disabled", !cardOnly);
                setStatusLbl();
                if (currentPlayer.includes("computer")) {
                    btnDice.prop("disabled", true);
                    btnCard.prop("disabled", true);
                    if (!cardOnly) {
                        throwDice();
                    }
                    chooseCard();
                }
                document.querySelector("#gameControls").classList.remove("d-none");
            }
        });
    }
    function chooseCard() {
        return __awaiter(this, void 0, void 0, function* () {
            if (currentStatus == gameStatus.Playing) {
                const rndIndex = Math.floor(Math.random() * lstCards.length);
                cardValue = lstCards[rndIndex].name;
                cardImg.src = `/Images/${cardValue}.jpg`;
                if (cardOnly) {
                    diceValue = lstCards[rndIndex].value;
                }
                btnCard.prop("disabled", true);
                setStatusLbl();
                if ($(document).width() < 768) {
                    yield delay(500);
                }
                document.querySelector("#gameControls").classList.add("d-none");
                takeTurn();
            }
        });
    }
    function throwDice() {
        if (currentStatus == gameStatus.Playing) {
            diceValue = Math.ceil(Math.random() * 6);
            diceImg.src = `/Images/dice${diceValue}.jpg`;
            btnDice.prop("disabled", true);
            btnCard.prop("disabled", false);
        }
    }
    function switchPlayer() {
        const $allSpotElements = $('[id^="spot"]');
        const currentPlayerIconClass = (currentPlayer === player1) ? player2Value : player1Value;
        $allSpotElements.each(function () {
            if ($(this).find(`.${currentPlayerIconClass}`).length > 0) {
                currentPlayerSpot = $(this);
                currentPlayerValue = currentPlayerIconClass;
                currentPlayer = (currentPlayer === player1) ? player2 : player1;
                return false;
            }
        });
    }
    function setStatusLbl() {
        switch (currentStatus) {
            case gameStatus.Playing:
                status.innerHTML = `Current Player = ${currentPlayer}; dice = ${diceValue}; card = ${cardValue}`;
                break;
            case gameStatus.Winner:
                status.innerHTML = `Winner = ${currentPlayer}`;
                break;
            case gameStatus.Loser:
                status.innerHTML = `Loser = ${currentPlayer}`;
                break;
            case gameStatus.NotStarted:
                status.innerHTML = "Please set up game modes by clicking start.";
                break;
        }
        setBtnText();
    }
})(gameTypeScript || (gameTypeScript = {}));
//# sourceMappingURL=game-typescript.js.map