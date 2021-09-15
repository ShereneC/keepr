<template>
  <div class="new-vault-modal">
    <!-- Modal -->
    <div class="modal"
         id="new-vault-modal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              Create New Vault
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="createVault">
              <div class="form-group">
                <label for="">Vault Name</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="name"
                       placeholder="Name..."
                       v-model="state.newVault.name"
                >
              </div>
              <div class="form-group">
                <label for="body">Description</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="description"
                       placeholder="Description..."
                       v-model="state.newVault.description"
                >
              </div>
              <div class="form-group">
                <label for="isPrivate" class="form-label">Private Vault</label>
                <input class="form-checkbox" type="checkbox" v-model="state.newVault.isPrivate">
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
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
import $ from 'jquery'
import { reactive } from '@vue/reactivity'

export default {

  setup() {
    const state = reactive({
      newVault: {
        // Do I need isPrivate here?  Is it so that it is defaulted to not private?
        isPrivate: false
      }
    })
    return {
      state,
      async createVault() {
        try {
          await vaultsService.createVault(state.newVault)
          state.newVault = { isPrivate: false }
          // again do I need to put isPrivate in here - is this default or will it overwrite what the form is doing?//
          $('#new-vault-modal').modal('hide')
          state.newVault = {}
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
