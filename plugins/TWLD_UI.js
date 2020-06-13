/*:
 * @plugindesc TWLDのUI
 * @help
 *     このプラグインはTWLDのUIモジュールやら共通で使ったりしそうな機能を提供する。
 *     ・OK/Cancel入力を受けてハンドラを呼び出すWindow_Simpleの追加
 *     ・共通で使えるコマンドウィンドウを構築するためのWindow_CommandGenericの追加。
 */
var Imported = Imported || {};
Imported.TWLD_UI = true;

var TWLD = TWLD || {};
TWLD.Core = TWLD.Core || {};


// for ESLint
if (typeof $gameParty === 'undefined') {
    var $dataSystem = {};
    var $gameParty = {};
    var $dataSkills = {};
    var Game_Item = {};
    var Game_BattlerBase = {};

    var Window_Base = {};
    var Window_MenuStatus = {};
    var Window_Command = {};
    var Window_Selectable = {};
    var Window_Help = {};
    var DataManager = {};
}



//------------------------------------------------------------------------------
// Window_Simple
// Window_Baseにハンドラとok,cancelだけ受けられるようにしたやつ。
// 
function Window_Simple() {
    this.initialize.apply(this, arguments);
}

Window_Simple.prototype = Object.create(Window_Base.prototype);
Window_Simple.prototype.constructor = Window_Simple;

/**
 * Window_Simpleを構築する
 * 
 * @param {Number} x ウィンドウ位置x
 * @param {Number} y ウィンドウ位置y
 * @param {Number} width ウィンドウ幅
 * @param {Number} height ウィンドウ高さ
 */
Window_Simple.prototype.initialize = function(x, y, width, height) {
    Window_Base.prototype.initialize.call(this, x, y, width, height);
    this._handlers = {};
    this._touching = false;
    this.deactivate();
};

/**
 * symbolで指定されるイベントに対応するハンドラを設定する。
 * @param {String} symbol シンボル
 * @param {Method} method メソッド
 */
Window_Simple.prototype.setHandler = function(symbol, method) {
    this._handlers[symbol] = method;
};

/**
 * symbolで指定されるイベントにハンドラが設定されているかを取得する。
 * 
 * @param {String} symbol シンボル
 * @return {Boolean} ハンドラが設定されている場合にはtrue、それ以外はfalse
 */
Window_Simple.prototype.isHandled = function(symbol) {
    return !!this._handlers[symbol];
}

/**
 * symbolに対応するハンドラを呼び出す。
 * 設定されていない場合には何もしない。
 * @param {String} symbol シンボル
 */
Window_Simple.prototype.callHandler = function(symbol) {
    if (this.isHandled(symbol)) {
        this._handlers[symbol]();
    }
};

/**
 * オープンしてアクティブ状態かどうかを取得する。
 * @return {Boolean} オープンかつアクティブな場合にはtrue。それ以外はfalse
 */
Window_Simple.prototype.isOpenAndActive = function() {
    return this.isOpen() && this.active;
};

/**
 * 更新する。
 */
Window_Simple.prototype.update = function() {
    Window_Base.prototype.update.call(this);
    this.processHandling();
    this.processTouch();
};

/**
 * 入力の処理をする。
 */
Window_Simple.prototype.processHandling = function() {
    if (this.isOpenAndActive()) {
        if (this.isOkEnabled() && this.isOkTriggered()) {
            this.processOk();
        } else if (this.isCancelEnabled() && this.isCancelTriggered()) {
            this.processCancel();
        }
    }
};

/**
 * タッチ処理をする。
 */
Window_Simple.prototype.processTouch = function() {
    if (this.isOpenAndActive()) {
        if (TouchInput.isTriggered() && this.isTouchedInsideFrame()) {
            this._touching = true;
            this.onTouch(true);
            if (this.isTouchOkEnabled()) {
                this.processOk();
            }
        } else if (TouchInput.isCancelled()) {
            if (this.isCancelEnabled()) {
                this.processCancel();
            }
        }
        if (this._touching) {
            if (TouchInput.isPressed()) {
                this.onTouch(false);
            } else {
                this._touching = false;
            }
        }
    } else {
        this._touching = false;
    }
};

/**
 * タッチ操作されているときの処理を行う。
 * @param {Boolean} triggered トリガーされているかどうか。
 * @return {Boolean} ？？？
 */
Window_Simple.prototype.onTouch = function(triggered) {
    return triggered && this.isTouchOkEnabled();
};

/**
 * タッチ操作が、このウィンドウ内の座標かどうかを判定する。
 * @return {Boolean} このウィンドウ内の座標の場合にはtrue, それ以外はfalse
 */
Window_Simple.prototype.isTouchedInsideFrame = function() {
    var x = this.canvasToLocalX(TouchInput.x);
    var y = this.canvasToLocalY(TouchInput.y);
    return x >= 0 && y >= 0 && x < this.width && y < this.height;
};

/**
 * タッチによるOK処理が有効かどうかを取得する。
 * @return {Boolean} タッチによるOK処理が有効な場合にはtrue, それ以外はfalse
 */
Window_Simple.prototype.isTouchOkEnabled = function() {
    return this.isOkEnabled();
};

/**
 * OKに対する処理が有効かどうかを取得する。
 * @return {Boolean} OKに対する処理が有効な場合にはtrue, それ以外はfalse
 */
Window_Simple.prototype.isOkEnabled = function() {
    return this.isHandled('ok');
};

/**
 * キャンセルに対する処理が有効かどうかを取得する。
 * @return {Boolean} キャンセルに対する処理が有効な場合にはtrue、それ以外はfalse
 */
Window_Simple.prototype.isCancelEnabled = function() {
    return this.isHandled('cancel');
};

/**
 * OKがトリガーされているかどうかを判定する。
 * @return {Boolean} OKがトリガーされている場合にはtrue, それ以外はfalse
 */
Window_Simple.prototype.isOkTriggered = function() {
    return Input.isRepeated('ok');
};

/**
 * キャンセルがトリガーされているかを判定する。
 * @return {Boolean} キャンセルがトリガーされている場合にはtrue, それ以外はfalse
 */
Window_Simple.prototype.isCancelTriggered = function() {
    return Input.isRepeated('cancel');
};

/**
 * OK処理をする。
 */
Window_Simple.prototype.processOk = function() {
    this.playOkSound();
    this.updateInputData();
    this.deactivate();
    this.callOkHandler();
};

/**
 * OK音を鳴らす。
 */
Window_Simple.prototype.playOkSound = function() {
    SoundManager.playOk();
};

/**
 * ブザー音を鳴らす。
 */
Window_Simple.prototype.playBuzzerSound = function() {
    SoundManager.playBuzzer();
};

/**
 * OKハンドラを呼び出す
 */
Window_Simple.prototype.callOkHandler = function() {
    this.callHandler('ok');
};

/**
 * キャンセル処理する。
 */
Window_Simple.prototype.processCancel = function() {
    SoundManager.playCancel();
    this.updateInputData();
    this.deactivate();
    this.callCancelHandler();
};

/**
 * キャンセルハンドラを呼び出す
 */
Window_Simple.prototype.callCancelHandler = function() {
    this.callHandler('cancel');
};

/**
 * 入力状態を更新する。
 */
Window_Simple.prototype.updateInputData = function() {
    Input.update();
    TouchInput.update();
};

/**
 * 描画内容を更新する。
 */
Window_Simple.prototype.refresh = function() {
    if (this.contents) {
        this.contents.clear();
        this.drawAllItems();
    }
};

//------------------------------------------------------------------------------
// GenericCommand
// 
/**
 * 汎用コマンド
 * @param {String} text 表示するテキスト
 * @param {String} symbol 通知を受け取るシンボル
 * @param {Boolean} enabled 有効かどうか。
 */
// eslint-disable-next-line no-unused-vars
function GenericCommand(text, symbol, enabled) {
    this.text = text || '';
    this.symbol = symbol || '';
    this.enabled = enabled || false;
}

//------------------------------------------------------------------------------
// Window_ItemCommand
// 毎回コマンドウィンドウ実装するのめんどくさい。
//
function Window_CommandGeneric() {
    this.initialize.apply(this, arguments);
}

Window_CommandGeneric.prototype = Object.create(Window_Command.prototype);
Window_CommandGeneric.prototype.initialize = Window_CommandGeneric;

/**
 * Window_ItemCommandを初期化する。
 * 
 */
Window_CommandGeneric.prototype.initialize = function(x, y, commandList, width) {
    this._windowWidth = width || 240;
    this._commandList = commandList;
    Window_Command.prototype.initialize.call(this, x, y);
    this.select(0);
};

/**
 * ウィンドウ幅を取得する。
 * @return {Number} ウィンドウ幅
 */
Window_CommandGeneric.prototype.windowWidth = function() {
    return this._windowWidth;
};

/**
 * 有効な行数を取得する。
 * @return {Number} 有効な行数。
 */
Window_CommandGeneric.prototype.numVisibleRows = function() {
    return (this._commandList) ? this._commandList.length : 0;
};

/**
 * コマンドリストを作成する。
 */
Window_CommandGeneric.prototype.makeCommandList = function() {
    if (!this._commandList) {
        return;
    }
    for (var i = 0; i < this._commandList.length; i++) {
        var cmdEntry = this._commandList[i];
        this.addCommand(cmdEntry.text, cmdEntry.symbol, cmdEntry.enabled);
    }
};

/**
 * 指定したシンボルのコマンドを有効にする。
 * @param {String} symbol シンボル
 * @param {Boolean} enabled 有効かどうか。
 */
Window_CommandGeneric.prototype.setCommandEnable = function(symbol, enabled) {
    if (this._commandList) {
        var cmdEntry = this._commandList.find(function(cmd) {
            return cmd.symbol === symbol;
        });
        if (cmdEntry !== null) {
            cmdEntry.enabled = enabled;
            this.refresh();
        }
    }
};

/**
 * indexで指定されるインデックスのコマンドが有効かどうかを取得する。
 * @param {Number} index インデックス
 * @return {Boolean} 有効な場合にはtrue, それ以外はfalse
 */
Window_CommandGeneric.prototype.isCommandEnabled = function(index) {
    if (this._commandList && (index >= 0) && (index < this._commandList.length)) {
        return this._commandList[index].enabled;
    } else {
        return false;
    }
};