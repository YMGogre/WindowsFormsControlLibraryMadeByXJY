window.onload = function() {
    var inputs = document.querySelectorAll('#ipv4-input input');
    inputs.forEach(function(input, index) {
        input.addEventListener('input', function(e) {
            this.value = this.value.replace(/[^0-9]/g, '');
            if (parseInt(this.value) > 255){
                this.value = '255';
            }
        });
        input.addEventListener('keydown', function(e) {
            if (e.keyCode === 37 || e.keyCode === 38) {
                if (this.selectionStart === 0 && index - 1 >= 0) {
                    inputs[index - 1].focus();
                    inputs[index - 1].selectionStart = inputs[index - 1].value.length;
                }
            }
            if (e.keyCode === 39 || e.keyCode === 40) {
                if (this.selectionStart === this.value.length && index + 1 < inputs.length) {
                    inputs[index + 1].focus();
                    inputs[index + 1].selectionStart = 0;
                }
            }
            if ((this.value.length === this.maxLength && e.keyCode !== 8 && e.keyCode !== 37 && e.keyCode !== 38 && e.keyCode !== 39 && e.keyCode !== 40) || (this.value.length > 0 && e.keyCode === 110)) {
                if (index + 1 < inputs.length) {
                    inputs[index + 1].focus();
                }
            }
            if (e.keyCode === 8 && this.value.length === 0) {
                if (index - 1 >= 0) {
                    inputs[index - 1].focus();
                }
            }
        });
    });
};