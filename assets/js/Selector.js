let selectedCircles = [];

class SelectableCircle {
    constructor(id) {
        this.id = id;
        this.index = null;
        this.element = document.createElement('div');
        this.element.className = 'circle';
        this.element.addEventListener('click', () => this.toggle());
    }

    select() {
        this.index = selectedCircles.length;
        selectedCircles.push(this);
        this.element.textContent = this.index + 1;
        this.element.classList.add('selected');
    }

    deselect() {
        selectedCircles.splice(this.index, 1);
        for (let i = this.index; i < selectedCircles.length; i++) {
            selectedCircles[i].index--;
            selectedCircles[i].element.textContent = selectedCircles[i].index + 1;
        }
        this.index = null;
        this.element.textContent = '';
        this.element.classList.remove('selected');
    }

    toggle() {
        if (this.index === null) {
            this.select();
        } else {
            this.deselect();
        }
    }
}

// 创建 8 个可选择的圆形元素
for (let i = 0; i < 8; i++) {
    let circle = new SelectableCircle(i);
    document.getElementById('container').appendChild(circle.element);
}