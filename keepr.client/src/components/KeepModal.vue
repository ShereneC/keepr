<template>
  <div class="modal" :id="'keep-modal' + keep.id" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
      <div class="modal-content container-fluid">
        <div class="row">
          <div class="col-md-6 p-0">
            <img class="pic h-100 p-1" :src="keep.img" alt="Keep image" style="width:100%">
          </div>
          <div class="col-md-6 d-flex flex-column">
            <div class="text-right pt-2">
              <p class="btn btn-secondary" data-dismiss="modal">
                X
              </p>
            </div>
            <div class="d-flex justify-content-around">
              <h6><span class="fa fa-eye">  </span>  {{ keep.views }}</h6>
              <h6 class="p-0 mb-2 align-items-center d-flex">
                <span><i class="material-icons">attachment</i></span>  {{ keep.keeps }}
              </h6>
              <h6><span class="fa fa-share-alt">  </span>{{ keep.shares }}</h6>
            </div>
            <div class="pb-1">
              <h2>{{ keep.name }}</h2>
            </div>
            <div class="d-flex">
              <h6 class="text-left">
                {{ keep.description }}
              </h6>
            </div>
            <div class="border-top p-1 min">
            </div>
            <div class="mt-auto pb-1 d-flex align-items-center justify-content-around pt-5">
              <form @submit.prevent="addKeepToVault(keep.id)" v-if="account.id">
                <div class="form-group">
                  <select
                    name="vaultName"
                    v-model="state.newVaultKeep.vaultId"
                    class="form-control"
                    :aria-describedby="'Vault Name'"
                  >
                    <option v-for="v in vaults" :key="v.id" :value="v.id">
                      {{ v.name }}
                    </option>
                  </select>
                  <button type="submit" class="btn btn-outline-secondary">
                    Add to Vault
                  </button>
                </div>
              </form>

              <p class="m-0 hoverable" v-if="account.id==keep.creatorId" :title="'Delete ' + keep.name" @click="deleteKeep">
                <span class="fa fa-trash"></span>
              </p>

              <div>
                <!-- NOTE these weren't showing up because activekeep hasn't been set on homepage load, and you can't dig into an undefined property.  So either need v-if or question mark says - does it exist? no? okay, don't dig in any further, just leave it as undefined -->
                <img :src="keep.creator?.picture" alt="profile image" class="profile-pic mt-auto ml-auto">
                {{ keep.creator?.name }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import $ from 'jquery'
import { router } from '../router'
import { vaultsService } from '../services/VaultsService'

export default {
  props: {
    // keep: { type: Object, required: true }
    // vault: { type: Object, required: true }
  },
  setup() {
    const state = reactive({
      newVaultKeep: {

      }
    })
    return {
      state,
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account),
      // keeps: computed(() => AppState.keeps),
      async addKeepToVault(kId) {
        try {
          await keepsService.addKeepToVault({ keepId: kId, vaultId: state.newVaultKeep.vaultId })
          // REVIEW mark said to research doing an sql increment count search
          // await keepsService.increaseKeepCount(AppState.activeKeep.id)
          Pop.toast(' has been added to your vault!', 'success')
          $('#keep-modal' + AppState.activeKeep.id).modal('hide')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.deleteKeep(AppState.activeKeep.id)
            $('#keep-modal' + AppState.activeKeep.id).modal('hide')
            Pop.toast('Successfully Deleted!', 'success')
            // router.push({ name: 'Home' })
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
      // clickMe(keepId) {
      // Just made this to see what was coming through from the state
      // console.log(keepId, state.newVaultKeep.vaultId)
      // }
    }
  }
}

</script>

<style lang="scss" scoped>
.line {
  border: 10px #000;
  width: 80%;
  margin: auto;
  margin-top: 5%;
  margin-bottom: 5%;
  }

  .box {
  flex-grow: 1;
  border: 3px solid rgba(0,0,0,.2);
}
.pic {

  // width: 8vw;
  object-fit: cover;
  border-radius: 2%
}

.profile-pic{
  height: 3rem;
  // width: 5vw;
  border-radius: 50%
  // object-fit: cover;
}

.hoverable {
  cursor: pointer;
}

.position {
  position: absolute;
  bottom: 8px;
  border: 3px solid #8AC007;
}

.min {
  flex: 1;
  min-height: 40vh;
}
</style>
