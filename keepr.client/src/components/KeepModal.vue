<template>
  <div class="modal" :id="'keep-modal' + keep.id" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
      <div class="modal-content container-fluid">
        <div class="modal-header row">
          <!-- Column for the image -->
          <div class="col-md-6">
            <img class="" :src="keep.img" alt="Keep image" style="width:100%">
          </div>
          <!-- Column for all other info -->

          <div class="col-md-6">
            <div class="row align-items-start flex-column">
              <div class="col-12 text-right">
                <p class="btn btn-secondary" data-dismiss="modal">
                  X
                </p>
              </div>
              <div class="col-12 d-flex justify-content-around">
                <h6><span class="fa fa-eye">  </span>  {{ keep.view }}</h6>
                <h6 class="p-0 m-0">
                  <span><i class="material-icons">attachment</i></span>  {{ keep.keeps }}
                </h6>
                <h6><span class="fa fa-share-alt">  </span>{{ keep.shares }}</h6>
              </div>
              <div class="col-12 pb-1">
                <h2>{{ keep.name }}</h2>
              </div>
              <!-- This row is for the dsecription only -->
              <!-- Stretch never works!!! Why won't it stretch down?   align-self-stretch -->
              <div class="col-12 d-flex">
                <h6 class="text-left">
                  {{ keep.description }}
                </h6>
              </div>
              <!-- Added this row just for a line -->
              <div class="col-12 border-bottom p-1">
              </div>
              <!-- Last row of info section - should be all the way down at the bottom -->
              <div class="col-12 d-flex mt-auto">
                <!-- REVIEW working on this.......  -->
                <!-- <h5>Choose Vault</h5>
                <div class>
                  <select>
                    <option v-for="v in vaults" :key="v.id" :value="v.id">
                      <h6>
                        {{ v.name }}  {{ v.id }}
                      </h6>
                    </option>
                  </select> -->
                <form @submit.prevent="addKeepToVault(keep.id)">
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
                      Click Me
                    </button>
                  </div>
                </form>

                <p class="m-0 hoverable" v-if="account.id==keep.creatorId" :title="'Delete ' + keep.name" @click="deleteKeep">
                  <span class="fa fa-trash"></span>
                </p>

                <div class="div">
                  <img :src="keep.creator.picture" alt="profile image" class="profile-pic mt-auto ml-auto">
                  {{ keep.creator.name }}
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- <h5 class="modal-title">
            {{ keep.name }} Details
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <p>Modal body text goes here.</p>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-primary">
            Save changes
          </button>
          <button type="button" class="btn btn-secondary" data-dismiss="modal">
            Close
          </button>
        </div>
      </div>
    </div>
  </div> -->
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
    keep: { type: Object, required: true }
    // vault: { type: Object, required: true }
  },
  name: 'KeepModal',
  setup(props) {
    const state = reactive({
      newVaultKeep: {

      }
    })
    onMounted(async() => {
      try {
        await vaultsService.getVaultsByProfileId(AppState.account.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      state,
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account),
      // keep: computed(() => AppState.activeKeep) - At this point in time, I am not using activeKeep, do I need to?  Not seeing where Mark used it in restaurants.
      async addKeepToVault(kId) {
        try {
          await keepsService.addKeepToVault({ keepId: kId, vaultId: state.newVaultKeep.vaultId })
          Pop.toast(' has been added to your vault!', 'success')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.deleteKeep(props.keep.id)
            $('#keep-modal' + props.keep.id).modal('hide')
            Pop.toast('Successfully Deleted!', 'success')
            // router.push({ name: 'Home' })
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      clickMe(keepId) {
        console.log(keepId, state.newVaultKeep.vaultId)
      }
    }
  },
  components: {}
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
.profile-pic{
  height: 3rem;
  // width: 5vw;
  border-radius: 50%
  // object-fit: cover;
}

.hoverable {
  cursor: pointer;
}
</style>
