<template>
  <div class="new-keep-modal">
    <!-- Modal -->
    <div class="modal"
         id="new-keep-modal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              Create New Keep
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="createKeep">
              <div class="form-group">
                <label for="">Keep Name</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="name"
                       placeholder="Name..."
                       v-model="state.newKeep.name"
                >
              </div>
              <div class="form-group">
                <label class="pr-2" for="imgUrl">Cover Image</label>
                <input type="text"
                       id="imgUrl"
                       class="form-control"
                       placeholder="Image Url..."
                       v-model="state.newKeep.img"
                >
              </div>
              <div class="form-group">
                <label for="body">Description</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="description"
                       placeholder="Description..."
                       v-model="state.newKeep.description"
                >
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                  Close
                </button>
                <button type="submit" class="btn btn-primary">
                  Save
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
import $ from 'jquery'
import { reactive } from '@vue/reactivity'

export default {

  setup() {
    const state = reactive({
      newKeep: {
        // Do I need isPrivate here?  Is it so that it is defaulted to not private?
      }
    })
    return {
      state,
      async createKeep() {
        try {
          await keepsService.createKeep(state.newKeep)
          // again do I need to put isPrivate in here - is this default or will it overwrite what the form is doing?//
          $('#new-keep-modal').modal('hide')
          state.newKeep = {}
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }
}

</script>

<style lang="scss" scoped>

</style>
