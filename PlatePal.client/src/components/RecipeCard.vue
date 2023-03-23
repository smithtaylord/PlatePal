<template>
    <div class="component">
        <!-- ONCLICK OPEN MODAL -->
        <div class="p-3 mt-4  rounded">
            <div class="position-relative rounded selectable recipe-box-shadow" title="click for recipe details">
                <img class="recipe-img rounded img-fluid " :src="recipe?.img" :alt="recipe?.title">
                <div
                    class="position-absolute bottom-0 start-0 glass-bg p-2 mb-3 ms-5 fs-3 text-center w-75 rounded recipe-box-shadow">
                    {{
                        recipe?.title }}
                </div>
                <div class="position-absolute top-0 start-5 w-100 d-flex justify-content-bwetwen">
                    <div class="glass-bg rounded recipe-box-shadow text-center ms-3 me-5 mt-3 fs-5 p-1 ">
                        {{
                            recipe?.category }}
                    </div>
                    <i class="mdi mdi-heart-outline glass-bg p-1 heart rounded recipe-box-shadow text-center ms-5 me-3 mt-3 fs-5"
                        title="favorite recipe"></i>
                </div>

            </div>
        </div>

    </div>
</template>


<script>
export default {
    props: {
        recipe: {
            type: Object,
            required: true
        }
    },
    setup() {
        return {}
    }
}
</script>


<style lang="scss" scoped>
.recipe-img {
    height: 35vh;
    width: 35vh;
    object-fit: cover;
}


.recipe-category {
    height: 5vh;
}

.glass-bg {
    color: white;
    text-shadow: 1px 1px 2px black;
    background-color: #404a4e6b;
    width: 100%;
    backdrop-filter: blur(7px)
}

.recipe-box-shadow {
    box-shadow: 0 4px 13px rgba(0, 0, 0, 0.253)
}

.heart {
    width: 10vh;
}
</style>