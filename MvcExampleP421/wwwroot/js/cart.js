let cartItems = [];

function loadFromLocalStorage() {
    const storedItems = localStorage.getItem('cartItems');
    if (storedItems) {
        cartItems = JSON.parse(storedItems);
    }
}

function saveToLocalStorage() {
    localStorage.setItem('cartItems', JSON.stringify(cartItems));
}

/**
 * Додає товар до корзини.
 * 
 * @param {id: number, name: string, quantity: number} item
 */
function addToCart(item) {
    const existingItem = cartItems.find(i => i.id === item.id);
    if (existingItem) {
        existingItem.quantity += item.quantity;
    } else {
        cartItems.push(item);
    }
    saveToLocalStorage();
    updateCartCount();
}

function removeFromCart(itemId) {
    cartItems = cartItems.filter(item => item.id !== itemId);
    saveToLocalStorage();
    showCart();
    updateCartCount();
}

function clearCart() {
    cartItems = [];
    saveToLocalStorage();
    updateCartCount();
}

async function makeOrder() {
    // Here you would typically send cartItems to your server for processing

    await fetch('/Cart/MakeOrder', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(cartItems)
    })
        .then(r => r.json())
        .then(order => {
            clearCart();
            Swal.fire({
                title: `Замовлення # ${order.id} оформлено`,
                text: 'Дякуємо за ваше замовлення!',
                icon: 'success',
                timer: 3000,
                showConfirmButton: false
            }).then(() => {
                window.location.href = `/Order/Index/${order.id}`;
            });
        });
}

async function showCart() {

    await fetch('/Cart/Render', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(cartItems)
    })
        .then(r => r.text())
        .then(html => {
            let el = document.getElementById('cart');
            el.innerHTML = html;

            document.querySelectorAll('[data-action="add-quantity"]')
                .forEach(btn => {
                btn.addEventListener('click', event => {
                    addQuantity(event.target.getAttribute('data-itemid')); 
                })
            })

            document.querySelectorAll('[data-action="sub-quantity"]').forEach(btn => {
                btn.addEventListener('click', event => {
                    subQuantity(event.target.getAttribute('data-itemid'));
                })
            })

            document.querySelectorAll('[data-action="remove-from-cart"]').forEach(btn => {
                btn.addEventListener('click', event => {
                    removeFromCart(event.target.getAttribute('data-itemid'));
                })
            })

            document.querySelectorAll('[data-action="make-order"]').forEach(btn => {
                btn.addEventListener('click', event => {
                    makeOrder();
                });
            })

        });
}

function updateCartCount() {
    document.querySelectorAll('span[data-cartcount]').forEach(el => {
        const count = cartItems.length;
        if (count > 0) {
            el.innerText = count;
            el.classList.remove('d-none');
        } else {
            el.innerText = "";
            el.classList.add('d-none');
        }
    })

}

function addQuantity(itemId) {
    const item = cartItems.find(i => i.id === itemId);
    if (item) {
        item.quantity += 1;
        saveToLocalStorage();
        showCart();
        updateCartCount();
    }
}

function subQuantity(itemId) {
    const item = cartItems.find(i => i.id === itemId);
    if (item) {
        item.quantity -= 1;
        if (item.quantity <= 0) {
            removeFromCart(itemId);
        } 
        saveToLocalStorage();
        showCart();
        updateCartCount();
    }
}

function initCart() {
    loadFromLocalStorage();
    updateCartCount();

    document.querySelectorAll('a[data-action="add-to-cart"]').forEach(link => {

        link.addEventListener('click', event => {
            const itemId = event.target.getAttribute('data-itemid');
            const itemName = event.target.getAttribute('data-itemname');

            addToCart({ id: itemId, name: itemName, quantity: 1 });

            Swal.fire({
                title: 'Додано до корзини',
                text: `${itemName}`,
                icon: 'success',
                timer: 2000,
                showConfirmButton: false
            });
        });
    })
}

initCart();