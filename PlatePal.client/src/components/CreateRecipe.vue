<template>
    <div class="component">
        <div class="modal-content">
            <div class="modal-header">
                <h1 class="modal-title fs-5" id="createRecipe">Create Recipe</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <form @submit.prevent="">

                    <div class="row">
                        <div class="col-6">
                            <div class="form-floating mb-3">
                                <input required type="text" class="form-control" id="name" for="name" placeholder="name..."
                                    maxlength="100" minlength="2">
                                <label for="floatingInput">Name</label>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-floating mb-3">
                                <input required type="text" class="form-control" id="cover-image" for="cover-image"
                                    placeholder="cover image..." maxlength="1000" minlength="2">
                                <label for="floatingInput">Cover Image</label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-floating">
                                <textarea required class="form-control" placeholder="Leave a comment here"
                                    id="event-description" for="event-description" style="height: 100px" maxlength="2500"
                                    minlength="2"></textarea>
                                <label for="floatingTextarea2">Event Description</label>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="reset" class="btn bg-secondary selectable" data-bs-dismiss="modal">Close</button>
                        <button type="submit" data-bs-dismiss="modal" class="btn bg-primary selectable">Submit</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>


<script>
export default {
    setup() {
        return {}
    }
}
</script>


<style lang="scss" scoped></style>