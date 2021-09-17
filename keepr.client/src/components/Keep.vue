<template>
  <div class="keep card img-fluid rounded elevation-1 bg-light my-3 text-left selectable">
    <img class="card-img-top" :src="keep.img" alt="Card image" style="width:100%">
    <div class="card-img-overlay d-flex">
      <h5 class="hoverable titleText text-red" title="Remove from vault" v-show="account.id==keep.creatorId &&homepage == false" @click="removeKeep">
        X
      </h5>
      <h4 class="card-title text-white titleText mt-auto"
          data-toggle="modal"
          :data-target="'#keep-modal' + keep.id"
          @click.prevent="setActiveKeep(keep.id)"
          title="See Keep Details"
      >
        <!-- REVIEW just put in setActive keep can I do that along with a modal? and do I need to do that? -->
        {{ keep.name }}
      </h4>
      <router-link class="mt-auto ml-auto" :to="{name: 'ProfilePage', params: {id: keep.creatorId}}">
        <img :src="keep.creator.picture" alt="profile image" class="profile-pic mt-auto ml-auto" title="Go to Profile Page">
      </router-link>
    </div>
  </div>
  <KeepModal />
  <!-- REVIEW  I was sending keeps as a prop to KeepModal , but it was blocking the updating of views in appstate so now it is getting it from activekeep, but it only opens the modal on the second click -->
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { useRoute, useRouter } from 'vue-router'
import $ from 'jquery'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'

export default {
  props: {
    keep: {
      type: Object,
      required: true
    }

  },
  setup(props) {
    const route = useRoute()
    const router = useRouter()
    return {
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account),
      homepage: computed(() => {
        if (route.path === '/' || route.path === '/profiles/613b964d03553023fe6ab5c4' || route.path === '/profiles/613b9e1845bd361e7d817142' || route.path === '/profiles/6133dba33605ba4cd98b9c35') {
          return true
        } else {
          return false
        }
      }),
      async setActiveKeep(id) {
        // console.log('hello from setactiveKeep on keep.vue')
        // console.log(id)
        await keepsService.getKeepById(id)
        await vaultsService.getVaultsByProfileId(AppState.account.id)
        // console.log(AppState.activeKeep)
        $('#keep-modal' + id).modal('show')
      },
      async removeKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.removeKeepFromVault(props.keep.vaultKeepId)
            await keepsService.getKeepsByVaultId(route.params.id)
            // NOTE I think I should be doing an appstate filter here instead of a call to the server again!!
            Pop.toast('Successfully Removed!', 'success')
            // router.reload()  -- why does this not work? I wanted to refresh the page
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }

    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.titleText {
  text-shadow: -1px -2px 0 #000;
  // REVIEW  text-shadow: 2px 2px -2 #000; - not happy about this shadow, it will not outline all around the textand it won't let me put it on twice - only takes the first.
}

.profile-pic{
  height: 3rem;
  // width: 5vw;
  border-radius: 50%
  // object-fit: cover;
}
</style>
