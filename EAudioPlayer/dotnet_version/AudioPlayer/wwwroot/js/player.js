(() => {
    const audio = document.getElementById('audio');
    const toggleBtn = document.getElementById('toggle-btn');
    const progress = document.getElementById('progress');
    const timeCurrent = document.getElementById('time-current');
    const timeTotal = document.getElementById('time-total');
    const tdTitle = document.getElementById('td-title');
    const tdAlbum = document.getElementById('td-album');
    const tdDetails = document.getElementById('td-details');
    const songsTitle = document.getElementById('songs-title');

    const albumButtons = [...document.querySelectorAll('.album-item')];
    const songLists = [...document.querySelectorAll('.song-list')];
    const songButtons = [...document.querySelectorAll('.song-item')];

    let current = null; // currently loaded song button
    let seeking = false;

    const fmtTime = s => {
        if (!isFinite(s)) return '0:00';
        s = Math.floor(s);
        return `${Math.floor(s / 60)}:${String(s % 60).padStart(2, '0')}`;
    };
    const fmtSize = b => b > 1048576 ? `${(b / 1048576).toFixed(1)} MB` : `${Math.max(1, Math.round(b / 1024))} KB`;

    function showAlbum(albumId) {
        albumButtons.forEach(b => b.classList.toggle('selected', b.dataset.album === albumId));
        songLists.forEach(l => { l.hidden = l.dataset.album !== albumId; });
        const btn = albumButtons.find(b => b.dataset.album === albumId);
        songsTitle.textContent = `Songs - ${btn.querySelector('.album-name').textContent}`;
    }

    function updateDescription() {
        if (!current) return;
        const d = current.dataset;
        tdTitle.textContent = d.title;
        tdAlbum.textContent = `${d.albumTitle} \u2022 Track ${d.number}`;
        const dur = isFinite(audio.duration) ? ` \u2022 ${fmtTime(audio.duration)}` : '';
        tdDetails.textContent = `${d.format} \u2022 ${fmtSize(+d.size)}${dur}`;
    }

    function updateSync() {
        const playing = !audio.paused && !audio.ended;
        toggleBtn.innerHTML = playing ? '&#9632; Stop' : '&#9654; Play';
        toggleBtn.classList.toggle('btn-danger', playing);
        toggleBtn.classList.toggle('btn-primary', !playing);
        toggleBtn.setAttribute('aria-pressed', playing);
        albumButtons.forEach(b => {
            const owns = playing && !!current && b.dataset.album === current.dataset.album;
            b.classList.toggle('playing', owns);
        });
        songButtons.forEach(b => b.classList.toggle('active', b === current));
    }

    function updateProgress() {
        const dur = audio.duration;
        const ratio = isFinite(dur) && dur > 0 ? audio.currentTime / dur : 0;
        timeCurrent.textContent = fmtTime(audio.currentTime);
        timeTotal.textContent = fmtTime(dur);
        if (!seeking) progress.value = Math.round(ratio * 1000);
        if (current) current.querySelector('.song-progress-bar').style.width = `${ratio * 100}%`;
    }

    function load(btn) {
        if (current) current.querySelector('.song-progress-bar').style.width = '0';
        current = btn;
        audio.src = btn.dataset.url;
        progress.disabled = false;
        updateDescription();
        updateSync();
        updateProgress();
    }

    function play() {
        audio.play().catch(err => { tdDetails.textContent = `Playback error: ${err.message}`; });
    }

    function nextSong() {
        const list = songButtons.filter(b => b.dataset.album === current.dataset.album);
        return list[list.indexOf(current) + 1] || null;
    }

    const visibleAlbumId = () => songLists.find(l => !l.hidden).dataset.album;

    // When nothing is playing, load the selected album's first track so the
    // description box and the Play button reflect the selected album.
    function syncIdleToSelectedAlbum() {
        const albumId = visibleAlbumId();
        if (audio.paused && (!current || current.dataset.album !== albumId)) {
            load(songButtons.find(b => b.dataset.album === albumId));
        }
    }

    // While a song is playing, selecting an album only browses it and does not interrupt playback.
    albumButtons.forEach(b => b.addEventListener('click', () => {
        showAlbum(b.dataset.album);
        syncIdleToSelectedAlbum();
    }));

    songButtons.forEach(b => b.addEventListener('click', () => {
        if (b !== current) load(b);
        play();
    }));

    // Toggle: stop playback (reset to start) or start/resume it.
    toggleBtn.addEventListener('click', () => {
        if (!audio.paused) {
            audio.pause();
            audio.currentTime = 0;
            syncIdleToSelectedAlbum();
        } else {
            syncIdleToSelectedAlbum();
            play();
        }
    });

    progress.addEventListener('input', () => {
        seeking = true;
        if (isFinite(audio.duration)) timeCurrent.textContent = fmtTime(progress.value / 1000 * audio.duration);
    });
    progress.addEventListener('change', () => {
        if (isFinite(audio.duration)) audio.currentTime = progress.value / 1000 * audio.duration;
        seeking = false;
    });

    audio.addEventListener('timeupdate', updateProgress);
    audio.addEventListener('loadedmetadata', () => { updateDescription(); updateProgress(); });
    audio.addEventListener('play', updateSync);
    audio.addEventListener('pause', () => { updateSync(); updateProgress(); });
    audio.addEventListener('error', () => { tdDetails.textContent = 'Unable to load this audio file.'; updateSync(); });
    audio.addEventListener('ended', () => {
        const next = nextSong();
        if (next) {
            // Keep the visible songs panel in sync when playback moves on.
            if (!songLists.find(l => !l.hidden).contains(next)) showAlbum(next.dataset.album);
            load(next);
            play();
        } else {
            updateSync();
            updateProgress();
        }
    });

    showAlbum(albumButtons[0].dataset.album);
    syncIdleToSelectedAlbum();
})();
